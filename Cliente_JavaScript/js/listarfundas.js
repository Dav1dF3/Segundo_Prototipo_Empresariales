// === LISTARFUNDAS.JS ===

// Al cargar la página
document.addEventListener("DOMContentLoaded", async () => {
    await cargarFundas();
    configurarFiltros();
  });
  
  // Cargar fundas desde API
  async function cargarFundas() {
    try {
      const fundas = await listarFundas(); // función de config.js
      renderizarTablaFundas(fundas);
    } catch (error) {
      console.error("Error cargando fundas:", error);
    }
  }
  
  // Renderizar tabla con fundas
  function renderizarTablaFundas(fundas) {
    const tbody = document.getElementById("tbody-case");
    tbody.innerHTML = "";
  
    if (!fundas || fundas.length === 0) {
      tbody.innerHTML = `<tr><td colspan="4">No hay fundas disponibles</td></tr>`;
      return;
    }
  
    fundas.forEach(funda => {
      const fila = document.createElement("tr");
  
      fila.innerHTML = `
        <td>${funda.codigo}</td>
        <td>${funda.codigo_guitarra}</td>
        <td>${funda.nombre || "-"}</td>
        <td>${funda.precio}</td>
      `;
  
      tbody.appendChild(fila);
    });
  }
  
  // Configurar filtros del modal
  function configurarFiltros() {
    const formFiltros = document.getElementById("form-filtros");
  
    formFiltros.addEventListener("submit", async (e) => {
      e.preventDefault();
  
      const filtro = {};
  
      const inputs = formFiltros.querySelectorAll("input[type='text'], input[type='number'], select");
  
      if (document.getElementById("chk-codigo-guitar").checked) filtro.codigoGuitarra = inputs[0].value.trim();
      if (document.getElementById("chk-nombre").checked) filtro.nombre = inputs[1].value.trim();
      if (document.getElementById("chk-preciomin").checked) filtro.precioMin = inputs[2].value;
      if (document.getElementById("chk-preciomax").checked) filtro.precioMax = inputs[3].value;
  
      try {
        // Igual que antes: en REST no es común GET con body, se usa POST para filtrar
        //FALTA CAMBIAR ESTO EN EL SERVIDOR
        //AGREGAR EL DTO PERO PARA FUNDAS, NO PARA INSTRUMENTOS
        const response = await fetch(`${BASE_URL}/filtrar`, {
          method: "POST",
          headers: headers, 
          body: JSON.stringify(filtro)
        });
  
        const fundas = await response.json();
        renderizarTablaFundas(fundas);
      } catch (error) {
        console.error("Error aplicando filtros:", error);
      }
    });
  }
  