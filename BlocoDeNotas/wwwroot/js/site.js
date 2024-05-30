// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function MostrarOpcoes()
{
    let opcoes = document.querySelector("#opcoes")
    opcoes.classList.toggle("mostrar")
}

$('.close-alert').click(function () {
    $('.alert').hide('hide')
})

document.addEventListener("DOMContentLoaded", function () {
    const notas = document.querySelectorAll(".preview-nota");

    notas.forEach(nota => {
        nota.addEventListener("click", function () {
            preencherFormularioComNota(this);
        });
    });
});

function preencherFormularioComNota(notaElement) {
    const tituloNota = document.getElementById("titulo");
    const conteudoNota = document.getElementById("descricao");
    const idNota = document.getElementById("id");

    const id = notaElement.getAttribute("data-id");
    const titulo = notaElement.getAttribute("data-titulo");
    const conteudo = notaElement.getAttribute("data-conteudo");

    tituloNota.value = titulo;
    conteudoNota.value = conteudo;
    idNota.value = id;
}

function confirmarExclusao(id) {
    if (confirm('Tem certeza de que deseja excluir esta nota?')) {
        document.getElementById('formExcluir-' + id).submit();
    }
}