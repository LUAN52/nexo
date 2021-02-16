var email = document.querySelector("#emailId");
var password = document.querySelector("#passwordId");
var button = document.querySelector("#registerId");
var errorEmail = document.querySelector("#emailErrorId"); 
var errorPassword = document.querySelector("#passwordErrorId");

console.log(button)
button.addEventListener("click",function(e)
{   
   
    console.log(password.value.length)
    console.log(email.value)

    var em = email.value;
    console.log(typeof em)
    const re =/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

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


