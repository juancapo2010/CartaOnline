//Cargar Mercaderia
var formularioMercaderiaCargar = document.getElementById('cargarMercaderiaForm');
var mercaderiaRespuestaCargar = document.getElementById('cargarMercaderiaRespuesta');

//const selectElement = document.querySelector('.selectTipo');
//selectElement.addEventListener('change', function (e) {
//    console.log(e.target.value)
//    var tipo = e.target.value  
//})
formularioMercaderiaCargar.addEventListener('submit', function (e) {
    e.preventDefault();
    var datos = new FormData(formularioMercaderiaCargar);
    console.log(e.target)
    let jsonDataConvert = JSON.stringify(
        {
            nombre: datos.get('nombre'),
            tipo: new Number(datos.get('tipo')),
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
            console.log(datos)
            if (!datos.ok) {
                mercaderiaRespuestaCargar.innerHTML=
                `<div class="alert alert-danger" role="alert">
                    Error
                </div>`
            } else {
                mercaderiaRespuestaCargar.innerHTML =
                    `<div class="alert alert-success" role="alert">
                    Se cargo Mercaderia
                </div>`
            }
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
            tipo: new Number(datos.get('tipo')),
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
            console.log(datos)
            if (!datos.ok) {
                mercaderiaRespuestaEditar.innerHTML =
                    `<div class="alert alert-danger" role="alert">
                    Error
                </div>`
            } else {
                mercaderiaRespuestaEditar.innerHTML =
                    `<div class="alert alert-success" role="alert">
                    Se Edito Mercaderia
                </div>`
            }
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
            console.log(datos)
            if (!datos.ok) {
                mercaderiaRespuestaEditar.innerHTML =
                    `<div class="alert alert-danger" role="alert">
                    Error
                </div>`
            } else {
                mercaderiaRespuestaEditar.innerHTML =
                    `<div class="alert alert-success" role="alert">
                    Se Elimino Mercaderia
                </div>`
            }
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
traerMercaderia()

