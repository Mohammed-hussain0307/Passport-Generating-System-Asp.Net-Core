// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#content-area").load("/User/AllUser");
});

$(document).ready(function () {
    $("#content-admin-area").load("/Admin/AllApplication");
});

$(document).ready(function () {
    $("#content-officer-area").load("/Officer/AllApplication");
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

function confirmDelete(userId) {
    if (confirm("Are you sure you want to delete this record?")) {
        fetch(`/User/Delete?id=${userId}`, {
            method: "DELETE"
        })
            .then(response => {
                if (response.ok) {
                    alert("Application successfully removed!");
                    location.reload();
                } else {
                    alert("Failed to delete user.");
                }
            })
    }
}