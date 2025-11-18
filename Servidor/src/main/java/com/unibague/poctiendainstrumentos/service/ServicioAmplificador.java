package com.unibague.poctiendainstrumentos.service;

import com.unibague.poctiendainstrumentos.dto.AmplificadorDTO;
import com.unibague.poctiendainstrumentos.dto.GuitarrasConAmplificadorDTO;
import com.unibague.poctiendainstrumentos.model.Guitarra;
import com.unibague.poctiendainstrumentos.repository.GuitarraRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

import java.util.List;

@Service
public class ServicioAmplificador {

    private final WebClient webClient;
    private final GuitarraRepository guitarraRepository;

    @Autowired
    public ServicioAmplificador(WebClient amplificadorWebClient, GuitarraRepository guitarraRepository) {
        this.webClient = amplificadorWebClient;
        this.guitarraRepository = guitarraRepository;
    }

    // Crear amplificador
    public Mono<AmplificadorDTO> crearAmplificador(AmplificadorDTO nuevoAmp) {
        return webClient.post()
                .uri("/amplificadores/")
                .bodyValue(nuevoAmp)
                .retrieve()
                .bodyToMono(AmplificadorDTO.class);
    }

    // Listar todos los amplificadores
    public Flux<AmplificadorDTO> obtenerTodos() {
        return webClient.get()
                .uri("/amplificadores/")
                .retrieve()
                .bodyToFlux(AmplificadorDTO.class);
    }

    // Consultar un amplificador por id
    public Mono<AmplificadorDTO> obtenerPorId(int id) {
        return webClient.get()
                .uri("/amplificadores/{id}", id)
                .retrieve()
                .bodyToMono(AmplificadorDTO.class);
    }

    // Actualizar amplificador por id
    public Mono<AmplificadorDTO> actualizarAmplificador(int id, AmplificadorDTO ampActualizado) {
        return webClient.put()
                .uri("/amplificadores/{id}", id)
                .bodyValue(ampActualizado)
                .retrieve()
                .bodyToMono(AmplificadorDTO.class);
    }

    // Eliminar amplificador por id
    public Mono<Void> eliminarAmplificador(int id) {
        return webClient.delete()
                .uri("/amplificadores/{id}", id)
                .retrieve()
                .bodyToMono(Void.class);
    }

    // Buscar amplificadores por marca (filtro)
    public Flux<AmplificadorDTO> buscarPorMarca(String marca) {
        return webClient.get()
                .uri(uriBuilder -> uriBuilder.path("/amplificadores/buscar/")
                        .queryParam("marca", marca)
                        .build())
                .retrieve()
                .bodyToFlux(AmplificadorDTO.class);
    }

    public Flux<GuitarrasConAmplificadorDTO> listarGuitarrasYAmplificadores(String filtroMarcaAmplificador) {
        List<Guitarra> listaGuitarras = guitarraRepository.findAll();

        Mono<List<AmplificadorDTO>> amplificadoresMono = (filtroMarcaAmplificador == null || filtroMarcaAmplificador.isEmpty())
                ? obtenerTodos().collectList()
                : buscarPorMarca(filtroMarcaAmplificador).collectList();

        Flux<Guitarra> guitarrasFlux = Flux.fromIterable(listaGuitarras);

        return guitarrasFlux.flatMap(guitarra -> amplificadoresMono.map(amps ->
                new GuitarrasConAmplificadorDTO(
                        guitarra.getMarca(),
                        guitarra.getMaterialCuerpo(),
                        amps
                )
        ));
    }

}
