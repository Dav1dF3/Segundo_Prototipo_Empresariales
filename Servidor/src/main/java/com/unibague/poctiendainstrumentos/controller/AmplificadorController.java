package com.unibague.poctiendainstrumentos.controller;

import com.unibague.poctiendainstrumentos.dto.AmplificadorDTO;
import com.unibague.poctiendainstrumentos.dto.GuitarrasConAmplificadorDTO;
import com.unibague.poctiendainstrumentos.service.ServicioAmplificador;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

@RestController
@RequestMapping("/amplificadores")
public class AmplificadorController {

    private final ServicioAmplificador servicioAmplificador;

    @Autowired
    public AmplificadorController(ServicioAmplificador servicioAmplificador) {
        this.servicioAmplificador = servicioAmplificador;
    }

    @PostMapping(value="/")
    @ResponseStatus(HttpStatus.CREATED)
    public Mono<AmplificadorDTO> crearAmplificador(@RequestBody AmplificadorDTO nuevoAmplificador) {
        return servicioAmplificador.crearAmplificador(nuevoAmplificador);
    }

    @GetMapping(value="/")
    public Flux<AmplificadorDTO> listarAmplificadores() {
        return servicioAmplificador.obtenerTodos();
    }

    @GetMapping(value="/{id}")
    public Mono<AmplificadorDTO> obtenerAmplificador(@PathVariable int id) {
        return servicioAmplificador.obtenerPorId(id);
    }

    @PutMapping(value="/{id}")
    public Mono<AmplificadorDTO> actualizarAmplificador(@PathVariable int id, @RequestBody AmplificadorDTO amplificadorActualizado) {
        return servicioAmplificador.actualizarAmplificador(id, amplificadorActualizado);
    }

    @DeleteMapping(value="/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public Mono<Void> eliminarAmplificador(@PathVariable int id) {
        return servicioAmplificador.eliminarAmplificador(id);
    }

    @GetMapping(value="/buscar")
    public Flux<AmplificadorDTO> buscarPorMarca(@RequestParam(required = false) String marca) {
        return servicioAmplificador.buscarPorMarca(marca);
    }

    @GetMapping(value="/listado/guitarras-amplificadores")
    public Flux<GuitarrasConAmplificadorDTO> obtenerGuitarrasYAmplificadores(
            @RequestParam(required = false) String marcaAmplificador) {
        return servicioAmplificador.listarGuitarrasYAmplificadores(marcaAmplificador);
    }
}
