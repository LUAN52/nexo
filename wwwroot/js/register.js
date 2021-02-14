var email = document.querySelector("#emailId");
var password = document.querySelector("#passwordId");
var button = document.querySelector("#registerId");
var errorEmail = document.querySelector("#emailErrorId"); 
var errorPassword = document.querySelector("#passwordErrorId");

console.log(button)
button.addEventListener("click",function(e)
{   
    e.preventDefault();
    console.log(password.value.length)
    console.log(email.value)
    const re = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i;
    if(!re.test(email.value))
    {
        e.preventDefault();
        errorEmail.innerHTML = "digite um email valido"
    }

    if(password.value.length<6)
    {
        e.preventDefault();
        errorPassword.innerHTML ="senha muito curta"
    }


})


