// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    let menuMobile = document.querySelector('.header__menu-mobile');
    let cerrarMenu = document.querySelector('.sidebar__cerrar');
    let sidebar = document.querySelector('.sidebar');

    if (menuMobile) {
        menuMobile.addEventListener('click', function () {
            sidebar.classList.add('mostrar');
        })
    }

    if (cerrarMenu) {
        cerrarMenu.addEventListener('click', function () {
            sidebar.classList.add('ocultar');
            setTimeout(() => {
                sidebar.classList.remove('mostrar');
                sidebar.classList.remove('ocultar');
            }, 500);
        })
    }

    window.addEventListener('resize', function () {
        const anchoPantalla = document.body.clientWidth;
        if (anchoPantalla >= 768) {
            sidebar.classList.remove('mostrar');
        }
    })
})