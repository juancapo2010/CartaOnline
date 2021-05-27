document.getElementById('socialClick').onclick = RedesSociales;
function RedesSociales(event){
    $('html,body').animate(
        {
            scrollTop: $('#social').offset().top
        }
    );
}
 //$(document).ready(() => {
 //    var ajax = new XMLHttpRequest();
 //    let URL = "https://localhost:44393/api/mercaderias";
 //    let METHOD = "GET";

 //    ajax.onreadystatechange = callback;
 //    ajax.open(METHOD,URL);
 //    afax.send(null);

 //    function callback(){
 //        if(ajax.readyState == 4) {
 //            if(ajax.status == 200)
 //                alert(JSON.stringify(ajax.responseText));
 //            else
 //                alert("Error");         
 //        }
 //    }

 //});

$(document).ready(function () {  
    $.ajax({  
        type: "GET",  
        url: "https://localhost:44393/api/Mercaderias",    
        dataType: "json", 
        success: function (data) {  
              $.each(data, function(i,item){
                  var row =
                      '<div id="card" class="col-sm-4">'+
                      '<div id="card" class="card" style="width: 18rem;">'+
                      '<img src="' + item.imagen + '" class="card-img-top" alt="...">'+
                      '<div class="card-body">'+
                      '<h4 class="card-title"><span>' + item.nombre +'</span></h4>'+
                          '<p class="card-text"><strong>Ingredientes: </strong>' + item.ingredientes +'<span></span></p>'+
                      '<p class="card-text"><span><strong>Prepracion: </strong>' + item.preparacion + '</span>.</p>' +
                      '<p class="card-text"><span><strong>Precio: </strong>' + item.precio + '</span>.</p>' +
                          '<a href="#" class="btn btn-primary">Ordenar</a>'+
                      '</div>' +
                      '</div >' +
                      '</div >'
                   $("#item>div").append(row);
               });
        }, //End of AJAX Success function  
        error: function () {
            console.log("No se ha podido obtener la información");
        }
    });         
});

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "https://localhost:44393/api/TipoMercaderias",
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
                var row =
                    '<button type="button" class="btn btn-outline-secondary" id="'+item.descripcion+'">' + item.descripcion+"</button>";
                $("#categorias>div").append(row);
            });
        }, //End of AJAX Success function  
        error: function () {
            console.log("No se ha podido obtener la información");
        }
    });
});
