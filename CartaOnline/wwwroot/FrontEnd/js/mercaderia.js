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
traerMercaderia()

