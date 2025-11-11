document.addEventListener("DOMContentLoaded", () => {
  const formFiltros = document.getElementById("form-filtros");
  const tablaBody = document.getElementById("tbody-case");

  formFiltros.addEventListener("submit", async (e) => {
    e.preventDefault();

    // Construir objeto filtro con todas las claves que el backend espera
    const filtro = {
      nombre: null,
      precioMin: null,
      precioMax: null,
      codigoGuitarra: null
    };

    // Codigo Guitarra
    if (document.getElementById("chk-codigo-guitar").checked) {
      const val = document.querySelector("#chk-codigo-guitar + label + input").value;
      filtro.codigoGuitarra = val !== "" ? parseInt(val) : null;
    }

    // Nombre
    if (document.getElementById("chk-nombre").checked) {
      const val = document.querySelector("#chk-nombre + label + input").value.trim();
      filtro.nombre = val !== "" ? val : null;
    }

    // Precio mínimo
    if (document.getElementById("chk-preciomin").checked) {
      const val = document.querySelector("#chk-preciomin + label + input").value;
      filtro.precioMin = val !== "" ? parseFloat(val) : null;
    }

    // Precio máximo
    if (document.getElementById("chk-preciomax").checked) {
      const val = document.querySelector("#chk-preciomax + label + input").value;
      filtro.precioMax = val !== "" ? parseFloat(val) : null;
    }

    console.log("Objeto filtro a enviar:", filtro);

    try {
      const response = await fetch(`${BASE_URL}/guitarras/fundas/filtrar`, {
        method: "POST",
        headers: headers,
        body: JSON.stringify(filtro)
      });

      if (!response.ok) {
        throw new Error("Error al aplicar filtros");
      }

      const data = await response.json();
      console.log("Respuesta del backend:", data);

      cargarTabla(data);

      document.getElementById("modal-filtros").style.display = "none";

    } catch (error) {
      console.error(error);
      alert("Hubo un error al filtrar las fundas ❌");
    }
  });

  function cargarTabla(funda) {
    tablaBody.innerHTML = "";

    if (!funda || funda.length === 0) {
      tablaBody.innerHTML = `<tr><td colspan="4">No se encontraron resultados</td></tr>`;
      return;
    }

    funda.forEach(inst => {
      const row = document.createElement("tr");

      row.innerHTML = `
        <td>${inst.codigo}</td>
        <td>${inst.codigoGuitarra}</td>
        <td>${inst.nombre || "-"}</td>
        <td>${inst.precio}</td>
      `;

      tablaBody.appendChild(row);
    });
  }
});
