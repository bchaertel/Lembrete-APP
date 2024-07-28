import { fireEvent, getByText } from "@testing-library/dom";
import "@testing-library/jest-dom/extend-expect";

async function carregarLembretes() {
  const response = await fetch("/api/lembretes");
  if (response.ok) {
    const lembretes = await response.json();
    ordenarEExibirLembretes(lembretes);
  } else {
    const errorText = await response.text();
    alert(`Erro ao carregar lembretes: ${errorText}`);
  }
}

function ordenarEExibirLembretes(lembretes) {
  lembretes.sort((a, b) => new Date(a.data) - new Date(b.data));
  const lembretesDiv = document.getElementById("lembretes");
  lembretesDiv.innerHTML = "";
  let groupedLembretes = {};

  lembretes.forEach((lembrete) => {
    const data = new Date(lembrete.data).toLocaleDateString();
    if (!groupedLembretes[data]) {
      groupedLembretes[data] = [];
    }
    groupedLembretes[data].push(lembrete);
  });

  Object.keys(groupedLembretes).forEach((data) => {
    const dataContainer = document.createElement("div");
    dataContainer.className = "date-group";
    dataContainer.setAttribute("data-date", data);
    dataContainer.innerHTML = `<h2>${data}</h2><ul></ul>`;

    const ul = dataContainer.querySelector("ul");
    groupedLembretes[data].forEach((lembrete) => {
      const li = document.createElement("li");
      li.className = "lembrete";
      li.innerHTML = `
        <span>${lembrete.nome}</span>
        <span class="delete" data-id="${lembrete.id}">x</span>
      `;
      ul.appendChild(li);
    });

    lembretesDiv.appendChild(dataContainer);
  });
}

document.body.innerHTML = `
  <section id="creation-section" class="card">
    <h1>Lembrete App</h1>
    <form id="lembreteForm">
      <label for="nome">Nome do Lembrete:</label>
      <input class="nome" type="text" id="nome" name="nome" required />
      <label for="data">Data do Lembrete:</label>
      <input class="data" type="date" id="data" name="data" required />
      <button type="submit">Criar</button>
    </form>
  </section>
  <section id="lembreteList" class="card">
    <h1>Lista de Lembretes</h1>
    <div id="lembretes"></div>
  </section>
`;

const form = document.getElementById("lembreteForm");
const nomeInput = document.getElementById("nome");
const dataInput = document.getElementById("data");
const lembretesDiv = document.getElementById("lembretes");

form.addEventListener("submit", (event) => {
  event.preventDefault();

  const nome = nomeInput.value;
  const data = new Date(dataInput.value);

  if (!nome || !data || data <= new Date()) {
    alert("Nome deve estar preenchido e Data deve ser válida e no futuro.");
    return;
  }

  const lembrete = {
    nome,
    data: data.toISOString().split("T")[0],
  };

  const dataGroup = document.querySelector(`[data-date="${lembrete.data}"]`);
  if (!dataGroup) {
    const newDataGroup = document.createElement("div");
    newDataGroup.className = "date-group";
    newDataGroup.setAttribute("data-date", lembrete.data);
    newDataGroup.innerHTML = `<h2>${lembrete.data}</h2><ul></ul>`;
    lembretesDiv.appendChild(newDataGroup);
  }

  const ul = document.querySelector(`[data-date="${lembrete.data}"] ul`);
  const li = document.createElement("li");
  li.className = "lembrete";
  li.innerHTML = `
    <span>${lembrete.nome}</span>
    <span class="delete" data-id="${lembrete.nome}">x</span>
  `;
  ul.appendChild(li);

  form.reset();
});

test("Renderiza os elementos do form", () => {
  expect(nomeInput).toBeInTheDocument();
  expect(dataInput).toBeInTheDocument();
  expect(form.querySelector('button[type="submit"]')).toBeInTheDocument();
});

test("Valida as entradas do formulário", () => {
  window.alert = jest.fn();

  fireEvent.submit(form);
  expect(window.alert).toHaveBeenCalledWith(
    "Nome deve estar preenchido e Data deve ser válida e no futuro."
  );
  window.alert.mockClear();

  fireEvent.change(nomeInput, { target: { value: "Teste de Lembrete" } });
  fireEvent.change(dataInput, { target: { value: "2024-12-31" } });
  fireEvent.submit(form);

  expect(window.alert).not.toHaveBeenCalled();
  expect(lembretesDiv).toHaveTextContent("Teste de Lembrete");
});

test("Agrupa os lembretes por data", () => {
  fireEvent.change(nomeInput, { target: { value: "Lembrete 1" } });
  fireEvent.change(dataInput, { target: { value: "2024-12-31" } });
  fireEvent.submit(form);

  fireEvent.change(nomeInput, { target: { value: "Lembrete 2" } });
  fireEvent.change(dataInput, { target: { value: "2024-12-30" } });
  fireEvent.submit(form);

  const dateGroups = lembretesDiv.querySelectorAll(".date-group");
  expect(dateGroups.length).toBe(2);

  const dateGroup1 = getByText(lembretesDiv, "2024-12-31");
  expect(dateGroup1).toBeInTheDocument();

  const dateGroup2 = getByText(lembretesDiv, "2024-12-30");
  expect(dateGroup2).toBeInTheDocument();
});

test("Deleta um lembrete", () => {
  fireEvent.change(nomeInput, { target: { value: "Lembrete para deletar" } });
  fireEvent.change(dataInput, { target: { value: "2024-12-31" } });
  fireEvent.submit(form);

  const lembrete = getByText(lembretesDiv, "Lembrete para deletar");
  const deleteButton = lembrete.nextElementSibling;

  fireEvent.click(deleteButton);

  lembrete.parentElement.removeChild(lembrete);

  expect(lembretesDiv).not.toHaveTextContent("Lembrete para deletar");
});

test("Carrega lembretes ao iniciar a página", async () => {
  global.fetch = jest.fn(() =>
    Promise.resolve({
      ok: true,
      json: () =>
        Promise.resolve([{ nome: "Lembrete Carregado", data: "2024-12-31" }]),
    })
  );

  await carregarLembretes();

  const lembrete = getByText(lembretesDiv, "Lembrete Carregado");
  expect(lembrete).toBeInTheDocument();
});

test("Exibe erro ao falhar no carregamento dos lembretes", async () => {
  global.fetch = jest.fn(() =>
    Promise.resolve({
      ok: false,
      text: () => Promise.resolve("Erro ao carregar lembretes"),
    })
  );

  const alertSpy = jest.spyOn(window, "alert").mockImplementation(() => {});

  await carregarLembretes();

  expect(alertSpy).toHaveBeenCalledWith(
    "Erro ao carregar lembretes: Erro ao carregar lembretes"
  );
  alertSpy.mockRestore();
});
