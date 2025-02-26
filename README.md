# Como subir o projeto para o Git

## Passos

1. Inicialize um repositório Git na pasta do seu projeto:
    ```sh
    git init
    ```

2. Adicione os arquivos ao repositório:
    ```sh
    git add .
    ```

3. Faça um commit com uma mensagem descritiva:
    ```sh
    git commit -m "Primeiro commit"
    ```

4. Adicione o repositório remoto:
    ```sh
    git remote add origin https://github.com/airtonomoreira/job-challenge.git
    ```

5. Faça o push dos commits para o repositório remoto:
    ```sh
    git push -u origin master
    ```

## Notas

- Você pode precisar configurar suas credenciais do Git se for a primeira vez que está usando o Git no seu sistema.
```