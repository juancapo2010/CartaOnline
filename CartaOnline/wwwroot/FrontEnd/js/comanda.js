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
        contenido.innerHTML += ` <tr>
                    <th scope="row">${valor.comandaId}</th>
                    <td>${valor.formaEntrega.descripcion}</td>
                    <td>${valor.precioTotal}</td>
                    <td>${valor.fecha.substr(0,16)}</td>
                </tr>`
    }
}
traerComandas()


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

    busqueda.innerHTML = `<div id="card" class="col-sm-4">
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