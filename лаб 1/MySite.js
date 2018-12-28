document.addEventListener('DOMContentLoaded', function(){
	centerDiv();
	window.onresize = centerDiv;
});

function centerDiv() {
	var mainTextDiv = document.querySelector(".mainText");
	var centeredDiv = document.querySelector(".centered");
	if(mainTextDiv  && centeredDiv) {
		centeredDiv.style.marginTop = (mainTextDiv.offsetHeight/2 - centeredDiv.offsetHeight/2)+"px";
		centeredDiv.style.marginLeft = (mainTextDiv.offsetWidth/2 - centeredDiv.offsetWidth/2)+"px";
	}
};