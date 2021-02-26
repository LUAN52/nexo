var nameForm = document.querySelector("#nameId");
var passwordForm = document.querySelector("#passwordId");

var nameError = document.querySelector("#nameErrorId");
var PasswordError = document.querySelector("#passwordErrorId");

var button = document.querySelector("#buttonLogin");

var loginUrl = "/Account/Login";
var ProductListUrl = "/Home/ProductList";

button.addEventListener("click", function (event) {
    event.preventDefault();

    var rest = loginValidation();
    if (!rest) {
        $.when(login())

    }


})


async function login() {

    await $.post(loginUrl, { userName: nameForm.value, password: passwordForm.value }, function (rest) {
        console.log(rest);


        if (rest) {

            window.location.replace(ProductListUrl);

        }
        else {
            PasswordError.innerHTML = "usuario ou senha incorreto";
            console.log(rest)
        }

    })

}


function loginValidation() {
    var control = false;
    if (nameForm.value == "") {
        nameError.innerHTML = "usuario em branco";
        control = true;
    }
    else {
        nameError.innerHTML = "";
    }

    if (passwordForm.value == "") {
        PasswordError.innerHTML = "senha em branco";
        control = true;
    }
    else {
        passwordForm.innerHTML = "";
    }

    return control;
}


