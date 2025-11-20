// === ACTUALIZARAMPLIFICADOR.JS ===

document.getElementById('btn-search')?.addEventListener('click', buscarHandler);
document.getElementById('btn-update')?.addEventListener('click', actualizarHandler);

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

function normalizarFecha(value) {
  if (!value) return '';
  if (/^\d{4}-\d{2}-\d{2}$/.test(value)) return value;
  try {
    const parts = value.split(/[\/\-]/);
    if (parts.length === 3) {
      if (parts[0].length === 4) return `${parts[0]}-${parts[1].padStart(2,'0')}-${parts[2].padStart(2,'0')}`;
      let [a,b,c] = parts;
      if (c.length === 2) c = '20' + c;
      const aNum = parseInt(a,10), bNum = parseInt(b,10);
      if (aNum <= 12 && bNum <= 31) return `${c}-${a.padStart(2,'0')}-${b.padStart(2,'0')}`; // mm/dd
      return `${c}-${b.padStart(2,'0')}-${a.padStart(2,'0')}`; // dd/mm
    }
  } catch(e) {
    console.warn('No se pudo normalizar fecha', value, e);
  }
  return value;
}

async function actualizarHandler() {
  const idRaw = document.getElementById('input-id')?.value.trim();
  if (!idRaw) { alert('Busca primero un amplificador'); return; }
  const id = parseInt(idRaw,10);
  if (isNaN(id)) { alert('El ID debe ser numérico'); return; }

  const marca = document.getElementById('input-marca').value.trim();
  const modelo = document.getElementById('input-modelo').value.trim();
  const potenciaRaw = document.getElementById('input-potencia').value.trim();
  const tipo_tubo = document.getElementById('input-tipo-tubo').value.trim();
  const fechaRaw = document.getElementById('input-fecha-fabricacion').value.trim();

  if (!marca || !modelo || !potenciaRaw || !tipo_tubo || !fechaRaw) {
    alert('⚠️ Todos los campos son obligatorios.');
    return;
  }

  const potencia = parseInt(potenciaRaw,10);
  if (isNaN(potencia)) { alert('Potencia debe ser numérica'); return; }

  const fecha_fabricacion = normalizarFecha(fechaRaw);
  const modificado = { id, marca, modelo, potencia, tipo_tubo, fecha_fabricacion };

  try {
    const resp = await editarAmplificador(id, modificado);
    if (resp.status === 404) { alert('No existe el amplificador (404)'); return; }
    if (resp.status === 409) { alert('Conflicto (409)'); return; }
    if (!resp.ok) throw new Error('Error al actualizar');
    alert('✅ Amplificador actualizado');
    limpiarCampos();
  } catch (e) {
    console.error(e);
    alert('Error al actualizar amplificador ❌');
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
