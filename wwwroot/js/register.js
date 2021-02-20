var emailF = document.querySelector("#emailId");
var nameF = document.querySelector("#nameId");
var passwordF = document.querySelector("#passwordId");

var button = document.querySelector("#registerId");

var errorEmail = document.querySelector("#emailErrorId"); 
var errorPassword = document.querySelector("#passwordErrorId");
var errorName = document.querySelector("#nameErrorId");

var testUrl ="/Account/TestEmail";
var registerUrl="/Account/Register"
var loginUrl ="/Account/Login";

console.log(button)
button.addEventListener("click",function(e)
{   
    formValidation(e)
      
})

function formValidation(e)
{
    const re =/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    var err=0
    e.preventDefault();
    if(!re.test(emailF.value))
    {   
        
        err=1
        errorEmail.innerHTML = "digite um email valido"
    }

    if(emailF.value=="")
    {
      
        err=1
        errorEmail.innerHTML ="email em branco"
    }

    if(passwordF.value.length<6)
    {
        
        err=1;
        errorPassword.innerHTML ="senha muito curta"
    }

    if(nameF.value=="")
    {   
        
        err=1;
        errorName.innerHTML ="nome em branco"
    }

    if(passwordF.value=="")
    {
        
        err=1;
        errorPassword.innerHTML= "senha em branco"
    }

    if(err===0)
    {   
      
       
        $.when(testEmail()).done(function(rest)
        {
           if(rest)
           {
               errorEmail.innerHTML ="email ja cadastrado";
               
           }
           else
           {
               register();
           }
        })    
    }
   
}

async function  testEmail(e)
{
 
        var rese;
       
        
        await $.post(testUrl,{email:emailF.value}).done(function(data){
            if(data)
            {
                rese = true;
            }
            else
            {
                rese = false;
            }
        })
       
   return  rese;   
       
}

async function register()
{
   
        await $.post(registerUrl,{name:nameF.value,email:emailF.value,password:passwordF.value}).done(function(something)
        {
            if(something)
            {
                window.location.replace(loginUrl);

            };

        })
}
