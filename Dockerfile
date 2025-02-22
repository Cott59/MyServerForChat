FROM c#
RUN pip install MyServerForChat
COPY F:\! СЕТЕВОЕ ПРОГРАММИРОВАНИЕ\20250206_ДЗ\MyServerForChat\MyServerForChat\bin\Release
WORKDIR /app
EXPOSE 5000
CMD ["MyServerForChat", "app.py"]