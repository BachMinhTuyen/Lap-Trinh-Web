const productWrapper = document.getElementById("productWrapper");
const btnList = document.getElementById("listViewBtn");
const btnGrid = document.getElementById("gridViewBtn");

btnList.addEventListener("click", () => {
	productWrapper.classList.add("list-view");
	btnList.classList.add("active");
	btnGrid.classList.remove("active");
});

btnGrid.addEventListener("click", () => {
	productWrapper.classList.remove("list-view");
	btnGrid.classList.add("active");
	btnList.classList.remove("active");
});
