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

function Sair() {
    swal({
        title: 'Deseja sair?',
        text: "Ao sair, a navegação será perdida.",
        icon: 'warning',
        buttons: ['Cancelar', true]
    }).then((result) => {
        if (result) {
            window.location.href = 'https://localhost:44370/Usuario/Logout';
        }
    });
}

function ExcluirConta(id) {
    swal({
        title: 'Deseja excluir?',
        text: "Ao excluir, não será possível recuperar a conta.",
        icon: 'warning',
        buttons: ['Cancelar', true]
    }).then((result) => {
        if (result) {
            window.location.href = 'https://localhost:7236/Usuario/Excluir/'+id;
        }
    });
}

function ExcluirNota(id) {
    swal({
        title: 'Deseja excluir?',
        text: "Ao excluir, não será possível recuperar a nota.",
        icon: 'warning',
        buttons: ['Cancelar', true]
    }).then((result) => {
        if (result) {
            window.location.href = 'https://localhost:7236/Notas/Excluir/' + id;
        }
    });
}