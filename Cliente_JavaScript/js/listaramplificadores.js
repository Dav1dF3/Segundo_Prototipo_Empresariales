// === LISTARAMPLIFICADORES.JS ===

document.addEventListener('DOMContentLoaded', async () => {
  await cargarAmplificadores();
  configurarFiltrosAmplificadores();
});

async function cargarAmplificadores() {
  try {
    const amplificadores = await listarAmplificadores();
    renderizarTablaAmplificadores(amplificadores);
  } catch (e) {
    console.error('Error cargando amplificadores:', e);
  }
}

function renderizarTablaAmplificadores(lista) {
  const tbody = document.getElementById('tbody-amplifier');
  tbody.innerHTML = '';

  if (!lista || lista.length === 0) {
    tbody.innerHTML = '<tr><td colspan="6">No hay amplificadores disponibles</td></tr>';
    return;
  }

  lista.forEach(a => {
    const tr = document.createElement('tr');
    tr.innerHTML = `
      <td>${a.id}</td>
      <td>${a.marca || '-'}</td>
      <td>${a.modelo || '-'}</td>
      <td>${a.potencia}</td>
      <td>${a.tipo_tubo || '-'}</td>
      <td>${a.fecha_fabricacion || '-'}</td>
    `;
    tbody.appendChild(tr);
  });
}

function configurarFiltrosAmplificadores() {
  const form = document.getElementById('form-filtros');
  if (!form) return; // seguridad

  form.addEventListener('submit', async (e) => {
    e.preventDefault();

    let lista = [];
    try {
      lista = await listarAmplificadores();
    } catch (e) {
      console.error('No se pudo obtener lista para filtrar:', e);
      return;
    }

    const filtro = {};
    const inMarca = form.querySelector('#chk-marca')?.checked ? form.querySelector('#chk-marca').closest('.filtro-item').querySelector('input[type=text]')?.value.trim() : '';
    const inModelo = form.querySelector('#chk-modelo')?.checked ? form.querySelector('#chk-modelo').closest('.filtro-item').querySelector('input[type=text]')?.value.trim() : '';
    const inPMin = form.querySelector('#chk-potenciamin')?.checked ? parseFloat(form.querySelector('#chk-potenciamin').closest('.filtro-item').querySelector('input[type=number]')?.value) : null;
    const inPMax = form.querySelector('#chk-potenciamax')?.checked ? parseFloat(form.querySelector('#chk-potenciamax').closest('.filtro-item').querySelector('input[type=number]')?.value) : null;
    const inTipoTubo = form.querySelector('#chk-tipo-tubo')?.checked ? form.querySelector('#chk-tipo-tubo').closest('.filtro-item').querySelector('input[type=text]')?.value.trim() : '';

    const filtrados = lista.filter(a => {
      if (inMarca && !a.marca?.toLowerCase().includes(inMarca.toLowerCase())) return false;
      if (inModelo && !a.modelo?.toLowerCase().includes(inModelo.toLowerCase())) return false;
      if (inPMin != null && Number(a.potencia) < inPMin) return false;
      if (inPMax != null && Number(a.potencia) > inPMax) return false;
      if (inTipoTubo && !a.tipo_tubo?.toLowerCase().includes(inTipoTubo.toLowerCase())) return false;
      return true;
    });

    renderizarTablaAmplificadores(filtrados);
  });
}
