// === ELIMINARAMPLIFICADOR.JS ===

document.getElementById('btn-search')?.addEventListener('click', buscarHandler);
document.getElementById('btn-delete')?.addEventListener('click', eliminarHandler);

async function buscarHandler() {
  const idRaw = document.getElementById('input-id')?.value.trim();
  if (!idRaw) { alert('Ingresa un ID para buscar'); return; }
  const id = parseInt(idRaw,10);
  if (isNaN(id)) { alert('El ID debe ser numérico'); return; }

  try {
    const amplificador = await buscarAmplificador(id);
    if (!amplificador) {
      alert('No se encontró amplificador con ese ID ❌');
      limpiarCampos(true);
      return;
    }
    document.getElementById('input-marca').value = amplificador.marca || '';
    document.getElementById('input-modelo').value = amplificador.modelo || '';
    document.getElementById('input-potencia').value = amplificador.potencia ?? '';
    document.getElementById('input-tipo-tubo').value = amplificador.tipo_tubo || '';
    document.getElementById('input-fecha-fabricacion').value = amplificador.fecha_fabricacion || '';
  } catch (e) {
    console.error(e);
    alert('Error al buscar amplificador ❌');
  }
}

async function eliminarHandler() {
  const idRaw = document.getElementById('input-id')?.value.trim();
  if (!idRaw) { alert('Busca primero un amplificador'); return; }
  const id = parseInt(idRaw,10);
  if (isNaN(id)) { alert('El ID debe ser numérico'); return; }

  const confirmar = confirm(`¿Eliminar amplificador ID ${id}? Esta acción no se puede deshacer.`);
  if (!confirmar) return;

  try {
    const resp = await eliminarAmplificador(id);
    if (resp.status === 404) { alert('No existe el amplificador (404)'); return; }
    if (!resp.ok) throw new Error('Error al eliminar');
    alert('✅ Amplificador eliminado');
    limpiarCampos();
  } catch (e) {
    console.error(e);
    alert('Error al eliminar amplificador ❌');
  }
}

function limpiarCampos(keepId) {
  if (!keepId) document.getElementById('input-id').value = '';
  document.getElementById('input-marca').value = '';
  document.getElementById('input-modelo').value = '';
  document.getElementById('input-potencia').value = '';
  document.getElementById('input-tipo-tubo').value = '';
  document.getElementById('input-fecha-fabricacion').value = '';
}
