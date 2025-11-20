// === BUSCARAMPLIFICADOR.JS ===

document.getElementById('btn-search')?.addEventListener('click', async () => {
  const idRaw = document.getElementById('input-id')?.value.trim();
  if (!idRaw) {
    alert('Ingresa un ID para buscar');
    limpiarCampos();
    return;
  }
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
});

function limpiarCampos(keepId) {
  if (!keepId) document.getElementById('input-id').value = '';
  document.getElementById('input-marca').value = '';
  document.getElementById('input-modelo').value = '';
  document.getElementById('input-potencia').value = '';
  document.getElementById('input-tipo-tubo').value = '';
  document.getElementById('input-fecha-fabricacion').value = '';
}
