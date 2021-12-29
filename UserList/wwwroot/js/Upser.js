// Get the button, and when the user clicks on it, execute myFunction
document.getElementById("btnUpsert").onclick = function () { BtnClick() };

function BtnClick() {
    //get the values from the input fields
    var id = $("#id").val();
    var name = $("#name").val();
    var age = $("#age").val();
    var city = $("#city").val();


    var obj = {
        ID: id,
        Name: name,
        Age: age,
        City: city

    }

    //call the submitForm and pass the parameter
    SubmitForm(obj)
}

function SubmitForm(obj) {
    $.ajax({
        url: "/Users/Upsert",
        method: "POST",
        data: obj,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                setInterval('location.reload()', 500);
            } else {
                toastr.error(data.message);
            }
        }
    })
}