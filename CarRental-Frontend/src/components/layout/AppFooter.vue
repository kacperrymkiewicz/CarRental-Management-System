<template>
  <footer>
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <div class="footer-container d-flex flex-column align-items-center">
            <div class="footer-brand d-flex justify-content-center align-items-center">
              <img src="/frontend/images/logo.png" class="img-fluid" alt="Logo">
              <div class="footer-brand-text d-flex flex-column">
                <span>Wypożyczalnia</span>
                <span>samochodów</span>
              </div>
            </div>
            <div class="footer-copyright">
              <p>© 2023 Wypożyczalnia samochodów</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<style lang="scss" scoped>
  .footer-brand img {
    width: 50px;
    margin-right: 10px;
  }
  .footer-brand-text {
    text-transform: uppercase;
    font-weight: 600;
    font-size: 20px;
    line-height: 24px;
  }

  footer {
    background-color: $primary;
    color: $secondary;
    padding: 30px 0;
    margin-top: 40px;
  }

  .footer-copyright {
    margin-top: 25px;
  }

  .footer-copyright p {
    margin: 0;
  }
</style>