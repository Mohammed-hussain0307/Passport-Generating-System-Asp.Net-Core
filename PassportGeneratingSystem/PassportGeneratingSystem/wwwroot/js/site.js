// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
	$(".load-page").click(function (e) {
		e.preventDefault(); // Prevent default link behavior

		var pageUrl = $(this).attr("href");

		$("#content-area").load(pageUrl); 
	});
});

$(document).ready(function () {
    var districtData = {
        "TamilNadu": ["Chennai", "Coimbatore", "Madurai", "Tenkasi"],
        "Kerala": ["Thiruvananthapuram", "Kochi", "Kozhikode"],
        "Rajasthan": ["Jaipur", "Udaipur", "Jodhpur"],
        "Karnataka": ["Bengaluru", "Mysuru", "Hubli"]
    };

    $("#stateDropdown").change(function () {
        var selectedState = $(this).val();
        var districtDropdown = $("#districtDropdown");
        districtDropdown.empty();
        districtDropdown.append('<option value="">Select District</option>');

        if (selectedState && districtData[selectedState]) {
            districtData[selectedState].forEach(function (district) {
                districtDropdown.append('<option value="' + district + '">' + district + '</option>');
            });
        }
    });
});
