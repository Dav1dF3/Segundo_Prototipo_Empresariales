// === MODAL FILTROS GENERICO ===
// Funciona para listados con estructura:
//  #btn-filtros, #modal-filtros, #form-filtros y .filtro-item { checkbox + label + input }
// Robusto ante ausencia de elementos (otras páginas reutilizan este JS).

document.addEventListener('DOMContentLoaded', () => {
  const btnFiltros = document.getElementById('btn-filtros');
  const modal = document.getElementById('modal-filtros');
  const spanClose = modal?.querySelector('.close');
  const form = document.getElementById('form-filtros');

  if (!btnFiltros || !modal || !form) return; // No hay modal en esta página

  // Asegurar que el modal inicia oculto aunque CSS falle
  modal.style.display = 'none';

  function abrirModal() {
    modal.classList.add('is-open');
    modal.style.display = 'flex';
    inicializarEstadoInputs();
    // Enfocar el primer checkbox para accesibilidad
    const firstChk = form.querySelector('input[type=checkbox]');
    firstChk?.focus();
  }

  function cerrarModal() {
    modal.classList.remove('is-open');
    modal.style.display = 'none';
  }

  btnFiltros.addEventListener('click', abrirModal);
  spanClose?.addEventListener('click', cerrarModal);

  // cerrar al hacer clic fuera
  window.addEventListener('click', (e) => {
    if (e.target === modal) cerrarModal();
  });

  // cerrar con ESC
  window.addEventListener('keydown', (e) => {
    if (e.key === 'Escape' && modal.style.display === 'block') cerrarModal();
  });

  // habilitar/deshabilitar inputs según checkbox (delegación)
  form.addEventListener('change', (e) => {
    const target = e.target;
    if (target.matches('input[type=checkbox]')) {
      const container = target.closest('.filtro-item');
      if (!container) return;
      const inputAsociado = container.querySelector('input:not([type=checkbox])');
      if (inputAsociado) inputAsociado.disabled = !target.checked;
    }
  });

  function inicializarEstadoInputs() {
    form.querySelectorAll('.filtro-item').forEach(container => {
      const chk = container.querySelector('input[type=checkbox]');
      const input = container.querySelector('input:not([type=checkbox])');
      if (chk && input) input.disabled = !chk.checked;
    });
  }

  // Inicialización temprana (por si el modal ya está visible via estilos)
  inicializarEstadoInputs();
});