//-----------------------------------------------------------------------------For Active Sidebar Item's
var menuItems = document.getElementsByClassName('menu-item')

Array.from(menuItems).forEach(
    function (menuItem) {
        menuItem.addEventListener('click', () => {
            ClearActiveMenuItem()
            menuItem.classList.toggle('active')
        })
    }
)

function ClearActiveMenuItem() {
    var menuItems = document.getElementsByClassName('menu-item')

    Array.from(menuItems).forEach(
        function (menuItem) {
            menuItem.classList.remove('active')
        }
    )
}
//------------------------------------------------------------------------------------------------------


//------------------------------------------------------------------------------------Hide Modal With Id
function hideModal(modalId) {

    const modalElement = document.getElementById(modalId);

    const modal = bootstrap.Modal.getInstance(modalElement);

    if (modal) {
        modal.hide();
    }

    const backdrop = document.querySelector('.modal-backdrop');

    if (backdrop) {
        backdrop.parentNode.removeChild(backdrop);
    }
}
//------------------------------------------------------------------------------------------------------