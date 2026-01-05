$(function () {

    // FORM SUBMIT
    $("#empForm").submit(function (e) {
        e.preventDefault();

        let isValid = true;

        let firstName = $("#firstName").val().trim();
        let lastName = $("#lastName").val().trim();
        let department = $("#department").val().trim();
        let email = $("#email").val().trim();

        // First Name validation
        if (firstName === "") {
            $("#firstNameError").text("First Name is required");
            isValid = false;
        } else {
            $("#firstNameError").text("");
        }

        // Last Name validation
        if (lastName === "") {
            $("#lastNameError").text("Last Name is required");
            isValid = false;
        } else {
            $("#lastNameError").text("");
        }

        // Department validation
        if (department === "") {
            $("#departmentError").text("Department is required");
            isValid = false;
        } else {
            $("#departmentError").text("");
        }

        // Email validation
        if (email === "") {
            $("#emailError").text("Email is required");
            isValid = false;
        }
        else if (!isValidEmail(email)) {
            $("#emailError").text("Enter a valid email");
            isValid = false;
        }
        else {
            $("#emailError").text("");
        }

       
        if (!isValid) {
            return;
        }

        // ✅ AJAX POST
        let employee = {
            firstName: firstName,
            lastName: lastName,
            department: department,
            email: email
        };

        $.ajax({
            url: "http://localhost:5237/api/employee",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(employee),
            success: function () {
                $("#msg").text("Employee added successfully!")
                         .removeClass("text-danger")
                         .addClass("text-success");

                $("#empForm")[0].reset();
            },
            error: function () {
                $("#msg").text("Error adding employee")
                         .removeClass("text-success")
                         .addClass("text-danger");
            }
        });
    });

    // LOAD EMPLOYEES
    $("#loadEmployees").click(function () {
        $.ajax({
            url: "http://localhost:5237/api/employee",
            type: "GET",
            success: function (data) {
                let rows = "";
                $.each(data, function (i, emp) {
                    rows += `<tr>
                        <td>${emp.id}</td>
                        <td>${emp.firstName}</td>
                        <td>${emp.lastName}</td>
                        <td>${emp.department}</td>
                        <td>${emp.email}</td>
                        <td>
                          <button class="btn btn-sm btn-primary editBtn"
                           data-id="${emp.id}"
                           data-firstname="${emp.firstName}"
                           data-lastname="${emp.lastName}"
                            data-department="${emp.department}"
                             data-email="${emp.email}">
                              Edit
                         </button>
                   <button class="btn btn-sm btn-danger deleteBtn ms-1"
                        data-id="${emp.id}">
                        Delete
                   </button>
                     </td>
                    </tr>`;
                });
                $("#empTable tbody").html(rows);
            },
            error: function () {
                alert("Error loading data");
            }
        });
    });//loademp

//Update Employee
$("#updateEmployee").click(function () {

    let id = $("#editId").val();

    let employee = {
        firstName: $("#editFirstName").val(),
        lastName: $("#editLastName").val(),
        department: $("#editDepartment").val(),
        email: $("#editEmail").val()
    };

    $.ajax({
        url: `http://localhost:5237/api/employee/${id}`,
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(employee),
        success: function () {
            $("#editModal").modal("hide");
            $("#loadEmployees").click(); // refresh table
        },
        error: function () {
            alert("Update failed");
        }
    });
});

//Delete employee
$("#confirmDelete").click(function () {

    let id = $("#deleteId").val();

    $.ajax({
        url: `http://localhost:5237/api/employee/${id}`,
        type: "DELETE",
        success: function () {

            let modalEl = document.getElementById("deleteModal");
            let modal = bootstrap.Modal.getInstance(modalEl);
            modal.hide();

            $("#loadEmployees").click();
        },
        error: function () {
            alert("Delete failed");
        }
    });
});

    $(document).on("click", ".editBtn", function () {
        console.log("Edit clicked", $(this).data("id"));
    $("#editId").val($(this).data("id"));
    $("#editFirstName").val($(this).data("firstname"));
    $("#editLastName").val($(this).data("lastname"));
    $("#editDepartment").val($(this).data("department"));
    $("#editEmail").val($(this).data("email"));
    let editModal = new bootstrap.Modal(document.getElementById("editModal"));
    editModal.show();
    });

    // 4️⃣ DELETE BUTTON CLICK (ADD HERE)
    $(document).on("click", ".deleteBtn", function () {
        console.log("Delete clicked", $(this).data("id"));
         $("#deleteId").val($(this).data("id"));

    let deleteModal = new bootstrap.Modal(
        document.getElementById("deleteModal")
    );
    deleteModal.show();
    });

});

// EMAIL VALIDATION FUNCTION
function isValidEmail(email) {
    let regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}
