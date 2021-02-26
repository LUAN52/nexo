var nameForm = document.querySelector("#nameId");
var passwordForm = document.querySelector("#passwordId");

var nameError = document.querySelector("#nameErrorId");
var PasswordError = document.querySelector("#passwordErrorId");

var button = document.querySelector("#buttonLogin");

var loginUrl = "/Account/Login";
var ProductListUrl = "/Home/ProductList";

button.addEventListener("click", function (event) {
    event.preventDefault();

  var rest=  loginValidation();
  if(!rest)
  {
    $.when(login()).done(function(rest)
    {
        if(rest)
        {
            PasswordError.innerHTML ="email ou senha incorreto";
        }
        else
        {
            window.location.replace(ProductListUrl);
        }
    })
  }


})


async function login() {

    await $.post(loginUrl, { userName: nameForm.value, password: passwordForm.value }) 
      
}


function loginValidation() {
    var control = false;
    if (nameForm.value == "") {
        nameError.innerHTML = "usuario em branco";
        control = true;
    }

    if (passwordForm.value == "") {
        PasswordError.innerHTML = "senha em branco";
        control = true;
    }

    return control;
}


