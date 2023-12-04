
function Validator (formSelector) {

    var _this = this
    //Chứa tất cả rule theo yêu cầu từ form
    var formRules = {}

    function getParent(element, selector) {
        while (element.parentElement) {
            if (element.parentElement.matches(selector)) {
                return element.parentElement
            }
            element = element.parentElement
        }
    }

    // Quy ước tạo rule:
    // - Nếu có lỗi thì return `error message1
    // - Nếu không có lỗi thì return `undefined`
    var validatorRules = {
        required: function(value) {
            return value ? undefined : 'Vui lòng nhập trường này'
        },
        email: function(value) {
            var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            return regex.test(value) ? undefined : 'Vui lòng nhập email'
        },
        min: function(min) {
            return function(value) {
                return value.length >= min ? undefined : `Vui lòng nhập tối thiểu ${min} ký tự`
            }
        },
        max: function(max) {
            return function(value) {
                return value.length <= max ? undefined : `Vui lòng nhập tối thiểu ${max} ký tự`
            }
        }
    }

    //Lấy ra form element trong DOM
    var formElement = document.querySelector(formSelector)

    //Xử lý khi có element
    if (formElement)
    {
        var inputs = formElement.querySelectorAll('[name][rules]')
        for (var input of inputs) {
            
            var rules = input.getAttribute('rules').split('|')
            for (var rule of rules ) {

                var ruleInfo
                var isRuleHasValue = rule.includes(':')
                
                if (isRuleHasValue) {
                    ruleInfo = rule.split(':');
                    rule = ruleInfo[0]
                }

                var ruleFunc = validatorRules[rule]

                if (isRuleHasValue) {
                    ruleFunc = ruleFunc(ruleInfo[1])
                }
                
                // console.log(rule)

                if (Array.isArray(formRules[input.name])) {
                    formRules[input.name].push(ruleFunc)
                }
                else {
                    formRules[input.name] = [ruleFunc]
                }
            }

            //Xử lý sự kiện để validate

            input.onblur = handleValidate
            input.oninput = handleClearError
        }

        //Hàm thực hiện validate
        function handleValidate(e) {
            var rules = formRules[e.target.name]
            var errorMessage;

            for (var rule of rules) {
                errorMessage = rule(e.target.value)
                if (errorMessage) break
            }

            //Nếu có lỗi -> hiển thị lỗi
            if (errorMessage) {

                var formGroup = getParent(e.target, '.form-group')
                if (formGroup) {
                    formGroup.classList.add('invalid')
                    
                    var formMessage = formGroup.querySelector('.form-message')
                    if (formMessage) {
                        formMessage.innerText = errorMessage
                    }
                }
            }

            return !errorMessage
        }

        //Hàm clear error message
        function handleClearError(e) {
            var formGroup = getParent(e.target, '.form-group')
            if (formGroup.classList.contains('invalid')) {
                formGroup.classList.remove('invalid')
                
                var formMessage = formGroup.querySelector('.form-message')
                if (formMessage) {
                    formMessage.innerText = ''
                }
            }
        }
    }

    //Xử lý hành vi submit form
    formElement.onsubmit = function(e) {
        e.preventDefault()

        var inputs = formElement.querySelectorAll('[name][rules]')
        var isValid = true
        
        for (var input of inputs) {
            if (!handleValidate({ target: input })) {
                isValid = false
            }
        }

        //Khi không có lỗi thì submit form
        if (isValid) {
            if (typeof _this.onSubmit === 'function') {
                
                var enableInputs = formElement.querySelectorAll('[name]:not([disabled])')
                var formValues = Array.from(enableInputs).reduce(function(values, input) {

                    switch(input.type) {
                        case 'radio':
                            values[input.name] = formElement.querySelector('input[name="' + input.name + '"]:checked').value
                            break
                        case 'checkbox':
                            if (!input.matches(':checked')) {
                                values[input.name] = ''
                                return values
                            }
                            if (!Array.isArray(values[input.name])) {
                                values[input.name] = []
                            }

                            values[input.name].push(input.value)
                            break
                        case 'file':
                            values[input.name] = input.files
                            break
                        default:
                            values[input.name] = input.value
                    }

                    return values
                }, {})
                

                //Gọi lại hàm và trả về các giá trị của form
                _this.onSubmit(formValues)
            }
            else {
                formElement.submit()
            }
        }
    }
}