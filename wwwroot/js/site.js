
var price = document.querySelector("#precoId");
var nameC = document.querySelector("#nameId");
var errorName = document.querySelector("#nameErrorId");
var errorPrice = document.querySelector("#priceErrorId");
var button = document.querySelector("#aqui")

console.log(button)

button.addEventListener("click", function (e) {
  
    console.log(nameC.value)
    if((price.value=="")||(nameC.value==""))
    {
        e.preventDefault();
        console.log(typeof(price))
        console.log("vazio")
        if (price.value =="") 
        {
            errorPrice.innerHTML = "nao pode deixar o valor em branco"
        }
    
        if(nameC.value=="")
        {
            errorName.innerHTML = "nao pode deixar o nome em braco"
        }
    }

    else
        var numb = price.value.replace(",",".");
        console.log(numb)
        if(isNaN(numb))
        {    e.preventDefault()
            errorPrice.innerHTML = " digite um valor numerico";

        }
        if( !typeof nameC.value=="string")
        {    e.preventDefault()
            errorName.innerHTML = "digite um nome valido"
        }

})



