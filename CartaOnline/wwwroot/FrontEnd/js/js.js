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
<button type="buttom" class="btn btn-outline-secondary"
id="${valor.tipoMercaderiaId}"
name="${valor.descripcion}"
title="${valor.tipoMercaderiaId}">${valor.descripcion}</button>`
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
                                  <h4 id="${valor.nombre}" class="card-title itemNombre"><span>${valor.nombre}</span></h4>
                                  <p id="${valor.ingredientes}" class="card-text itemIngredientes"><strong>Ingredientes: </strong>${valor.ingredientes}<span></span></p>
                                  <p id="${valor.preparacion}" class="card-text itemPreparacion"><span><strong>Prepracion: </strong>${valor.preparacion}</span></p>
                                  <p id="${valor.precio}" class="card-text itemPrecio"><span><strong>Precio: </strong>${valor.precio}</span></p>
                                  <button id="${valor.id}" class="btn btn-primary itemButtom">Ordenar</button>
                             </div>
                        </div >
                      </div >`
    }
}
TraerMercaderias()

//Filtrar por categoria
var formularioFiltrarMercaderia = document.getElementById('filtrarMercaderia');

var contenidoItem = document.querySelector('#item')

    formularioFiltrarMercaderia.addEventListener('click', function (e) {
        e.preventDefault();
        console.log(e.target.id)
        var id = e.target.id
        fetch(`https://localhost:44393/api/mercaderia/?tipo=${id}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        })
            .then(res => res.json())
            .then(datos => {
                console.log(datos)
                CardMercaderia(datos)
            })
    })

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
                            <h4 id="${mercaderia.nombre}" class="card-title itemNombre"><span>${mercaderia.nombre}</span></h4>
                            <p id="${mercaderia.ingredientes}" class="card-text itemIngredientes"><strong>Ingredientes: </strong>${mercaderia.ingredientes}<span></span></p>
                            <p id="${mercaderia.preparacion}" class="card-text itemPreparacion"><span><strong>Prepracion: </strong>${mercaderia.preparacion}</span></p>
                            <p id="${mercaderia.precio}" class="card-text itemPrecio"><span><strong>Precio: </strong>${mercaderia.precio}</span></p>
                            <button id="${mercaderia.id}" class="btn btn-primary itemButtom">Ordenar</button>
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
    //console.log(e.target)
    if (e.target.classList.contains('itemButtom')) {
        setCarrito(e.target.parentElement)
    }
    e.stopPropagation()
}
const setCarrito = objeto => {
    //console.log(objeto)
    const producto = {
        id: objeto.querySelector('.itemButtom').id,
        nombre: objeto.querySelector('.itemNombre').id,
        precio: objeto.querySelector('.itemPrecio').id,
        cantidad: 1
    }
    
    if (carrito.hasOwnProperty(producto.id)) {
        producto.cantidad = carrito[producto.id].cantidad + 1
    }
    carrito[producto.id] = { ...producto }
    console.log(producto)
    alert("Se agrego al carrito")
    pintarCarrito()
}
const itemsCarrito = document.querySelector('#contenidoCarrito')
const templateCarrito = document.getElementById('template-carrito').content
const templateFooter = document.getElementById('template-footer').content
const footer = document.getElementById('footer')

const fragment = document.createDocumentFragment()

const pintarCarrito = () => {
    itemsCarrito.innerHTML = ''

    Object.values(carrito).forEach(producto => {
        templateCarrito.querySelector('th').textContent = producto.id
        templateCarrito.querySelectorAll('td')[0].textContent = producto.nombre
        templateCarrito.querySelectorAll('td')[1].textContent = producto.cantidad
        templateCarrito.querySelector('span').textContent = producto.precio * producto.cantidad

        //botones
        templateCarrito.querySelector('.buttomMas').dataset.id = producto.id
        templateCarrito.querySelector('.buttomMenos').dataset.id = producto.id

        const clone = templateCarrito.cloneNode(true)
        fragment.appendChild(clone)
    })
    itemsCarrito.appendChild(fragment)

    pintarFooter()
    }

const pintarFooter = () => {
    footer.innerHTML = ''

    if (Object.keys(carrito).length === 0) {
        footer.innerHTML = `
        <th scope="row" colspan="5">Carrito vacio con innerHTML</th>
        `
        return
    }

    // sumar cantidad y sumar totales
    const nCantidad = Object.values(carrito).reduce((acc, { cantidad }) => acc + cantidad, 0)
    const nPrecio = Object.values(carrito).reduce((acc, { cantidad, precio }) => acc + cantidad * precio, 0)
    // console.log(nPrecio)

    templateFooter.querySelectorAll('td')[0].textContent = nCantidad
    templateFooter.querySelector('span').textContent = nPrecio

    const clone = templateFooter.cloneNode(true)
    fragment.appendChild(clone)

    footer.appendChild(fragment)

    const boton = document.querySelector('#vaciar-carrito')
    boton.addEventListener('click', () => {
        carrito = {}
        pintarCarrito()
    })
}
const btnAumentarDisminuir = e => {
    // console.log(e.target.classList.contains('btn-info'))
    if (e.target.classList.contains('buttomMas')) {
        const producto = carrito[e.target.id]
        console.log(carrito[e.target.id])
        producto.cantidad++
        carrito[e.target.id] = { ...producto }
        pintarCarrito()
    }

    if (e.target.classList.contains('buttomMenos')) {
        const producto = carrito[e.target.id]
        producto.cantidad--
        if (producto.cantidad === 0) {
            delete carrito[e.target.dataset.id]
        } else {
            carrito[e.target.id] = { ...producto }
        }
        pintarCarrito()
    }
    e.stopPropagation()
}
