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