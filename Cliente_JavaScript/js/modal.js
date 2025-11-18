const btnFiltros = document.getElementById("btn-filtros");
    const modal = document.getElementById("modal-filtros");
    const spanClose = document.querySelector(".close");

    // abrir modal
    btnFiltros.onclick = function () {
      modal.style.display = "block";
    }

    // cerrar modal
    spanClose.onclick = function () {
      modal.style.display = "none";
    }

    // cerrar al hacer clic fuera
    window.onclick = function (event) {
      if (event.target == modal) {
        modal.style.display = "none";
      }
    }

    // habilitar/deshabilitar inputs según checkbox
    document.querySelectorAll("#form-filtros input[type=checkbox]").forEach(chk => {
      chk.addEventListener("change", function () {
        // buscar el input asociado dentro del mismo contenedor .filtro-item
        const container = this.closest('.filtro-item');
        if (!container) return;
        // seleccionar el primer input que no sea checkbox (texto/número)
        const targetInput = container.querySelector('input:not([type=checkbox])');
        if (targetInput) targetInput.disabled = !this.checked;
      });
    });

    // estado inicial: deshabilitar los inputs que tengan su checkbox sin marcar
    document.querySelectorAll('#form-filtros .filtro-item').forEach(container => {
      const chk = container.querySelector('input[type=checkbox]');
      const target = container.querySelector('input:not([type=checkbox])');
      if (chk && target) target.disabled = !chk.checked;
    });