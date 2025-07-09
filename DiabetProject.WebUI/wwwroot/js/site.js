document.addEventListener("DOMContentLoaded", function () {
    var sidebar = document.getElementById("sidebar");
    var toggleButton = document.getElementById("toggleSidebar");
    var content = document.getElementById("content");

    if (sidebar && toggleButton) {
        toggleButton.addEventListener("click", function () {
            sidebar.classList.toggle("collapsed");
            document.body.classList.toggle("sidebar-closed");
            document.body.classList.toggle("sidebar-open");
        });
    } else {
        console.error("Sidebar veya toggle butonu bulunamadı!");
    }
});

document.getElementById("toggleSidebar").addEventListener("click", function () {
    let sidebar = document.getElementById("sidebar");

    if (sidebar.classList.contains("hidden")) {
        sidebar.classList.remove("hidden");
        this.style.left = "210px";  // Butonu sağa kaydır
    } else {
        sidebar.classList.add("hidden");
        this.style.left = "1px";   // Butonu sola getir
    }
});

const toggleBtn = document.getElementById("toggleSidebar");
toggleBtn?.addEventListener("click", () => {
    document.body.classList.toggle("sidebar-open");
    
    document.body.classList.toggle("auth-collapsed");
});