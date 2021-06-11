//Listar todas las Comandas
var contenido = document.querySelector('#contenido')

function traerComandas() {
    fetch(`https://localhost:44393/api/comanda/`)
        .then((res) => res.json())
        .then((data) => {
            //console.log(data)
            tabla(data)
        });
}

function tabla(data) {
    //console.log(data)
    contenido.innerHTML = ''
    for (let valor of data) {
        var cantidad =0;
        for (let mercaderias of valor.mercaderia) {
            cantidad = cantidad + 1;
        }
        contenido.innerHTML += ` <tr>
                    <th scope="row">${valor.comandaId}</th>
                    <td><a href="#" id="${valor.comandaId}" class="listado" type="button" class="btn btn-link" data-bs-toggle="modal" data-bs-target="#listarMercaderia">
                        ${cantidad} Unidades</td></a>
                    <td>${valor.formaEntrega.descripcion}</td>
                    <td>${valor.precioTotal}</td>
                    <td>${valor.fecha.substr(0,16)}</td>
                </tr>`
    }
}
traerComandas()

//Listar Mercaderias de Comanda
document.addEventListener('click', e => {
    verComanda(e)
})
const verComanda = e => {
    if (e.target.classList.contains('listado')) {
        consultarMercaderias(e.target.id)
    }
    e.stopPropagation()
}
function consultarMercaderias(id) {
    fetch(`https://localhost:44393/api/comanda/${id}`)
        .then((res) => res.json()
        )
        .then((data) => listarMercaderiaByComanda(data));
}
function listarMercaderiaByComanda(object) {
    console.log(object)
    let contenido = document.querySelector('#listado')
    contenido.innerHTML = ''
    for (let valor of object.mercaderia) {
        contenido.innerHTML += `<div class="card" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">${valor.nombre}</h5>
    <h6 class="card-subtitle mb-2 text-muted">${valor.ingredientes}</h6>
    <p class="card-text">${valor.preparacion}</p>
  </div>
</div>`
    }
}

//buscarComandaPor Id
document.querySelector("#buscarComanda").addEventListener('click', (e) => {
    e.preventDefault();
    traerComanda(document.querySelector("#inputComanda").value);
})
function traerComanda(id) {
    fetch(`https://localhost:44393/api/comanda/${id}`)
        .then((res) => res.json()
        )
        .then((data) => crearComandaItem(data));
}

function crearComandaItem(comanda) {
    const busqueda = document.querySelector("#busquedaComanda");
    busqueda.innerHTML = ''

    busqueda.innerHTML = `
               <div id="card" class="col-sm-4">
                <div id="card" class="card" style="width: 18rem;">
                        <div class="card-body">
                            <h4 class="card-title"><span># ${comanda.comandaId}</span></h4>
                            <p class="card-text"><strong>Forma de Entraga: </strong>${comanda.formaEntrega.descripcion}<span></span></p>
                            <p class="card-text"><span><strong>Prepracion: </strong>${comanda.fecha}</span>.</p>
                            <p class="card-text"><span><strong>Precio Total: </strong>${comanda.precioTotal}</span>.</p>
                        </div>
                 </div >
               </div >`
}