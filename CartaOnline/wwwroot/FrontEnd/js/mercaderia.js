//Cargar Mercaderia
var formularioMercaderiaCargar = document.getElementById('cargarMercaderiaForm');
var mercaderiaRespuestaCargar = document.getElementById('cargarMercaderiaRespuesta');

formularioMercaderiaCargar.addEventListener('submit', function (e) {
    e.preventDefault();
    var datos = new FormData(formularioMercaderiaCargar);
    console.log(e.target)
    let jsonDataConvert = JSON.stringify(
        {
            nombre: datos.get('nombre'),
            tipo: new Number(document.getElementById("TipoMercaderia").value),
            precio: new Number(datos.get('precio')),
            ingredientes: datos.get('ingredientes'),
            preparacion: datos.get('preparacion'),
            imagen: datos.get('imagen')
        }               
    );
    console.log(jsonDataConvert)

    fetch(`https://localhost:44393/api/mercaderia`, {
        method: 'POST',
        body: jsonDataConvert,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(res => res.json())
        .then(datos => {
            alert("Se cargo mercaderia")
            location.reload()
        })
})
//Editar Mercaderia
var formularioMercaderiaEditar = document.getElementById('editarMercaderiaForm');
var mercaderiaRespuestaEditar = document.getElementById('editarMercaderiaRespuesta');

formularioMercaderiaEditar.addEventListener('submit', function (e) {
    e.preventDefault();
    var datos = new FormData(formularioMercaderiaEditar);

    let jsonDataConvert = JSON.stringify(
        {
            nombre: datos.get('nombre'),
            tipo: new Number(document.getElementById("TipoMercaderiaEditar").value),
            precio: new Number(datos.get('precio')),
            ingredientes: datos.get('ingredientes'),
            preparacion: datos.get('preparacion'),
            imagen: datos.get('imagen')
        }
    );
    console.log(jsonDataConvert)
    var id = datos.get('id');
    fetch(`https://localhost:44393/api/mercaderia/${id}`, {
        method: 'PUT',
        body: jsonDataConvert,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(res => res.json())
        .then(datos => {
            alert("Se edito Mercaderia")
            location.reload()
        })
})

//Eliminar Mercaderia
var formularioMercaderiaEliminar = document.getElementById('eliminarMercaderiaForm');
var mercaderiaRespuestaEliminar = document.getElementById('eliminarMercaderiaRespuesta');

formularioMercaderiaEliminar.addEventListener('submit', function (e) {
    e.preventDefault();
    var datos = new FormData(formularioMercaderiaEliminar);

    var id = datos.get('id');
    fetch(`https://localhost:44393/api/mercaderia/${id}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(res => res.json())
        .then(datos => {
            alert("Se elimino mercaderia")
            location.reload()
        })
})
//ListarMercaderia
var contenidoMercaderia = document.querySelector('#contenidoMercaderia')

function traerMercaderia() {
    fetch(`https://localhost:44393/api/mercaderia/`)
        .then((res) => res.json())
        .then((data) => {
            //console.log(data)
            tablaMercaderia(data)
        });
}

function tablaMercaderia(data) {
    //console.log(data)
    contenidoMercaderia.innerHTML = ''
    for (let valor of data) {
        contenidoMercaderia.innerHTML += ` <tr>
                    <th scope="row">${valor.id}</th>
                    <td>${valor.nombre}</td>
                    <td>${valor.tipo}</td>
                    <td>${valor.precio}</td>
                    <td>${valor.ingredientes}</td>
                    <td>${valor.preparacion}</td>
                    <td><img src="${valor.imagen}" alt="" width="50" height="50"></td>
                </tr>`
    }
}
window.onload = traerMercaderia()

//Buscar Mercaderia
document.querySelector("#buscarMercaderiaPanel").addEventListener('click', (e) => {
    e.preventDefault();
    traerMercaderiaPanel(document.querySelector("#inputPanel").value);
})
function traerMercaderiaPanel(id) {
    fetch(`https://localhost:44393/api/mercaderia/${id}`)
        .then((res) => res.json()
        )
        .then((data) => crearMercaderiaItemPanel(data));
}

function crearMercaderiaItemPanel(mercaderia) {
    const busquedaPanel = document.querySelector("#busquedaPanel");
    busquedaPanel.innerHTML = ''
    busquedaPanel.innerHTML = `
                <div id="card" class="">
                    <div id="card" class="card" style="width: 25rem;">
                    <img src="${mercaderia.imagen}" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h4 id="${mercaderia.nombre}" class="card-title itemNombre"><span>${mercaderia.nombre}</span></h4>
                            <p id="${mercaderia.ingredientes}" class="card-text itemIngredientes"><strong>Ingredientes: </strong>${mercaderia.ingredientes}<span></span></p>
                            <p id="${mercaderia.preparacion}" class="card-text itemPreparacion"><span><strong>Prepracion: </strong>${mercaderia.preparacion}</span></p>
                            <p id="${mercaderia.precio}" class="card-text itemPrecio"><span><strong>Precio: </strong>${mercaderia.precio}</span></p>
                        </div>
                    </div >
                </div >`
}
function mostrarMercaderia() {
    let x = $("#inputMercaderiaPanel").val();
    console.log(x);
    FiltrarMercaderia(x);
}
function FiltrarMercaderia(valor) {
    let URL = `https://localhost:44393/api/mercaderia/?tipo=${valor}`
    console.log(URL);
    fetch(URL, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(res => res.json())
        .then(mercaderia => {
            tablaMercaderia(mercaderia)
        })
}
FiltrarMercaderia();
