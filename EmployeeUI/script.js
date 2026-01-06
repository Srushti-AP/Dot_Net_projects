$(function () {

    /* ============================
       ADD EMPLOYEE FORM VALIDATION
       ============================ */
    $("#empForm").validate({
        rules: {
            firstName: {
                required: true,
                minlength: 2
            },
            lastName: {
                required: true,
                minlength: 2
            },
            department: {
                required: true
            },
            email: {
                required: true,
                email: true
            }
        },
        messages: {
            firstName: {
                required: "First name is required",
                minlength: "Minimum 2 characters required"
            },
            lastName: {
                required: "Last name is required",
                minlength: "Minimum 2 characters required"
            },
            department: {
                required: "Department is required"
            },
            email: {
                required: "Email is required",
                email: "Enter a valid email address"
            }
        },
        errorElement: "span",
        errorClass: "text-danger",
        highlight: function (element) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element) {
            $(element).removeClass("is-invalid");
        },
        submitHandler: function (form) {
            addEmployee();
        }
    });

    /* ============================
       LOAD EMPLOYEES
       ============================ */
    $("#loadEmployees").click(function () {
        loadEmployees();
    });

    /* ============================
       UPDATE EMPLOYEE
       ============================ */
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
                loadEmployees();
            },
            error: function () {
                alert("Update failed");
            }
        });
    });

    /* ============================
       CONFIRM DELETE
       ============================ */
    $("#confirmDelete").click(function () {

        let id = $("#deleteId").val();

        $.ajax({
            url: `http://localhost:5237/api/employee/${id}`,
            type: "DELETE",
            success: function () {
                let modalEl = document.getElementById("deleteModal");
                let modal = bootstrap.Modal.getInstance(modalEl);
                modal.hide();
                loadEmployees();
            },
            error: function () {
                alert("Delete failed");
            }
        });
    });

    /* ============================
       EDIT BUTTON CLICK
       ============================ */
    $(document).on("click", ".editBtn", function () {
        $("#editId").val($(this).data("id"));
        $("#editFirstName").val($(this).data("firstname"));
        $("#editLastName").val($(this).data("lastname"));
        $("#editDepartment").val($(this).data("department"));
        $("#editEmail").val($(this).data("email"));

        let editModal = new bootstrap.Modal(
            document.getElementById("editModal")
        );
        editModal.show();
    });

    /* ============================
       DELETE BUTTON CLICK
       ============================ */
    $(document).on("click", ".deleteBtn", function () {
        $("#deleteId").val($(this).data("id"));

        let deleteModal = new bootstrap.Modal(
            document.getElementById("deleteModal")
        );
        deleteModal.show();
    });

});

/* ============================
   ADD EMPLOYEE AJAX
   ============================ */
function addEmployee() {

    let employee = {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        department: $("#department").val(),
        email: $("#email").val()
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
            $(".form-control").removeClass("is-invalid");
        },
        error: function () {
            $("#msg").text("Error adding employee")
                .removeClass("text-success")
                .addClass("text-danger");
        }
    });
}

/* ============================
   LOAD EMPLOYEES AJAX
   ============================ */
function loadEmployees() {

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
            alert("Error loading employees");
        }
    });
}
