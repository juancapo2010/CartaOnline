document.getElementById('socialClick').onclick = RedesSociales;
function RedesSociales(event){
    $('html,body').animate(
        {
            scrollTop: $('#social').offset().top
        }
    );
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "https://localhost:44393/api/TipoMercaderia",
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
                var row =
                    '<button type="button" class="btn btn-outline-secondary" id="' + item.tipoMercaderiaId + '" title="' + item.tipoMercaderiaId + '">' + item.descripcion + "</button>";
                $("#categorias>div").append(row);
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
        url: `https://localhost:44393/api/mercaderia/`,    
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

document.querySelector("#buscarMercaderia").addEventListener('click', (e) => {
    e.preventDefault();
    traerMercaderia(document.querySelector("#input").value);
})
function traerMercaderia(id) {
    fetch(`https://localhost:44393/api/mercaderia/${id}`)
        .then((res) => res.json()
        )
        .then((data) => crearMercaderiaItem(data));
}

function crearMercaderiaItem(mercaderia) {
    const busqueda = document.querySelector("#busqueda");
    busqueda.innerHTML = ''

    busqueda.innerHTML = `<div id="card" class="col-sm-4">
                <div id="card" class="card" style="width: 18rem;">
                    <img src="${mercaderia.imagen}" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h4 class="card-title"><span>${mercaderia.nombre}</span></h4>
                            <p class="card-text"><strong>Ingredientes: </strong>${mercaderia.ingredientes}<span></span></p>
                            <p class="card-text"><span><strong>Prepracion: </strong>${mercaderia.preparacion}</span>.</p>
                            <p class="card-text"><span><strong>Precio: </strong>${mercaderia.precio}</span>.</p>
                            <a href="#" class="btn btn-primary">Ordenar</a>
                        </div>
        </div >
                </div >`
}