//Redessociales
document.getElementById('socialClick').onclick = RedesSociales;
function RedesSociales(event){
    $('html,body').animate(
        {
            scrollTop: $('#social').offset().top
        }
    );
}
//Categorias
var contenidoCategoria = document.querySelector('#categorias')
function TraerTipoMercaderia() {
    fetch(`https://localhost:44393/api/TipoMercaderia/`)
        .then((res) => res.json())
        .then((data) => {
            botonesTipoMercaderia(data)
        });
}
function botonesTipoMercaderia(data) {
    contenidoCategoria.innerHTML = ''
    for (let valor of data) {
        contenidoCategoria.innerHTML += `
<button type="button" class="btn btn-outline-secondary" id="${valor.tipoMercaderiaId}" title="${valor.tipoMercaderiaId}">${valor.descripcion}</button>`
    }
}
TraerTipoMercaderia()

//Item
var contenidoItem = document.querySelector('#item')
function TraerMercaderias() {
    fetch(`https://localhost:44393/api/mercaderia/`)
        .then((res) => res.json())
        .then((data) => {
            CardMercaderia(data)
        });
}
function CardMercaderia(data) {
    contenidoItem.innerHTML = ''
    for (let valor of data) {
        contenidoItem.innerHTML += `
                      <div id="card" class="col-sm-6 col-md-5 col-lg-4">
                        <div id="card" class="card" style="width: 18rem;">
                            <img src="${valor.imagen}" class="card-img-top" width="200" height="150" alt="...">
                            <div class="card-body">
                                  <h4 class="card-title"><span>${valor.nombre}</span></h4>
                                  <p class="card-text"><strong>Ingredientes: </strong>${valor.ingredientes}<span></span></p>
                                  <p class="card-text"><span><strong>Prepracion: </strong>${valor.preparacion}</span></p>
                                  <p class="card-text"><span><strong>Precio: </strong>${valor.precio}</span></p>
                                  <button class="btn btn-primary item">Ordenar</button>
                             </div>
                        </div >
                      </div >`
    }
}
TraerMercaderias()
//Buscar Mercaderia
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

    busqueda.innerHTML = `
                <div id="card" class="">
                    <div id="card" class="card" style="width: 25rem;">
                    <img src="${mercaderia.imagen}" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h4 class="card-title"><span>${mercaderia.nombre}</span></h4>
                            <p class="card-text"><strong>Ingredientes: </strong>${mercaderia.ingredientes}<span></span></p>
                            <p class="card-text"><span><strong>Prepracion: </strong>${mercaderia.preparacion}</span></p>
                            <p class="card-text"><span><strong>Precio: </strong>${mercaderia.precio}</span></p>
                            <button class="btn btn-primary item">Ordenar</button>
                        </div>
                    </div >
                </div >`
}
//Carrito
let carrito = {}
document.addEventListener('click', e => {
    addCarrito(e)
})
const addCarrito = e => {
    console.log(e.target)
    if (e.target.classList.contains('item')) {
        console.log(e.target.parentElement)
    }
    e.stopPropagation()
}
const setCarrito = objeto => {
    
}