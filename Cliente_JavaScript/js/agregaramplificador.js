// === AGREGARAMPLIFICADOR.JS ===

document.addEventListener('DOMContentLoaded', () => {
  const btn = document.getElementById('btn-add-amplifier');
  if (btn) btn.addEventListener('click', agregarAmplificadorHandler);
});

function normalizarFecha(value) {
  if (!value) return '';
  // Intentar detectar formato datepicker mm/dd/yy o dd/mm/yy y pasar a yyyy-mm-dd
  // Si ya viene en yyyy-mm-dd lo retornamos igual
  if (/^\d{4}-\d{2}-\d{2}$/.test(value)) return value;
  try {
    const parts = value.split(/[\/\-]/);
    if (parts.length === 3) {
      // Heurística: si la primera parte tiene 4 dígitos es año
      if (parts[0].length === 4) {
        return `${parts[0]}-${parts[1].padStart(2,'0')}-${parts[2].padStart(2,'0')}`;
      }
      // Asumir formato mm/dd/yy o dd/mm/yy => tomar año último (yy)
      let [a,b,c] = parts;
      if (c.length === 2) c = '20' + c; // expandir año corto
      // Decidir si a es mes (1-12)
      const aNum = parseInt(a,10);
      const bNum = parseInt(b,10);
      if (aNum <= 12 && bNum <= 31) { // mm/dd/yy
        return `${c}-${a.padStart(2,'0')}-${b.padStart(2,'0')}`;
      } else { // dd/mm/yy
        return `${c}-${b.padStart(2,'0')}-${a.padStart(2,'0')}`;
      }
    }
  } catch(e) {
    console.warn('No se pudo normalizar fecha', value, e);
  }
  return value;
}

async function agregarAmplificadorHandler() {
  const idRaw = document.getElementById('input-id')?.value.trim();
  const marca = document.getElementById('input-marca')?.value.trim();
  const modelo = document.getElementById('input-modelo')?.value.trim();
  const potenciaRaw = document.getElementById('input-potencia')?.value.trim();
  const tipo_tubo = document.getElementById('input-tipo-tubo')?.value.trim();
  const fechaRaw = document.getElementById('input-fecha-fabricacion')?.value.trim();

  if (!idRaw || !marca || !modelo || !potenciaRaw || !tipo_tubo || !fechaRaw) {
    alert('⚠️ Completa todos los campos obligatorios.');
    return;
  }

  const id = parseInt(idRaw,10);
  const potencia = parseInt(potenciaRaw,10);
  if (isNaN(id) || isNaN(potencia)) {
    alert('ID y Potencia deben ser numéricos.');
    return;
  }

  const fecha_fabricacion = normalizarFecha(fechaRaw);

  const amplificador = { id, marca, modelo, potencia, tipo_tubo, fecha_fabricacion };
  console.log('JSON a enviar:', amplificador);

  try {
    const resp = await agregarAmplificador(amplificador);
    if (resp.status === 409) {
      alert('❌ Ya existe un amplificador con ese ID.');
      return;
    }
    if (!resp.ok) throw new Error('Error en creación');
    alert('✅ Amplificador agregado correctamente');
    limpiar();
  } catch (e) {
    console.error(e);
    alert('Error al agregar amplificador ❌');
  }

  function limpiar() {
    document.getElementById('input-id').value = '';
    document.getElementById('input-marca').value = '';
    document.getElementById('input-modelo').value = '';
    document.getElementById('input-potencia').value = '';
    document.getElementById('input-tipo-tubo').value = '';
    document.getElementById('input-fecha-fabricacion').value = '';
  }
}
