<template>
  <div class='centered-container'>
    <md-content class='md-elevation-3'>

      <div class='title'>
        <img src='https://vuematerial.io/assets/logo-color.png'>
        <div class='md-title'>Savings Calculator</div>
        <div class='md-body-1'>A really <strong>COOL</strong> savings app! 👍</div>
      </div>

      <div class='form'>
        <md-field>
          <label>First Name</label>
          <md-input v-model='register.firstName' autofocus></md-input>
        </md-field>

        <md-field>
          <label>Last Name</label>
          <md-input v-model='register.lastName'></md-input>
        </md-field>

        <md-field>
          <label>E-mail</label>
          <md-input v-model='register.email'></md-input>
        </md-field>

        <md-field md-has-password>
          <label>Password</label>
          <md-input v-model='register.password' type='password'></md-input>
        </md-field>

        <md-field md-has-password>
          <label>Confirm Password</label>
          <md-input v-model='register.confirmPassword' type='password'></md-input>
        </md-field>
      </div>

      <div class='actions md-layout md-alignment-center-space-between'>
        <span>Already a user? <router-link to="../login">Login</router-link></span>
        <md-button class='md-raised md-primary' @click='registerUser'>Sign Up</md-button>
      </div>

      <div class='loading-overlay' v-if='loading'>
        <md-progress-spinner md-mode='indeterminate' :md-stroke='2'></md-progress-spinner>
      </div>

    </md-content>
    <div class='background' />
  </div>
</template>

<script>
export default {
  name: 'Register',
  data () {
    return {
      loading: false,
      register: {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: ''
      }
    }
  },
  methods: {
    registerUser () {
      // TODO:
      this.$store.dispatch('register', { ...this.register })
        .then(() => this.$router.push('/'))
        .catch(err => console.log(err))
        .finally(() => (this.loading = false))
    }
  }
}
</script>

<style lang='scss'>
  .centered-container {
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    height: 100vh;

    .title {
      text-align: center;
      margin-bottom: 30px;
      img {
        margin-bottom: 16px;
        max-width: 80px;
      }
      .md-title {
        margin-bottom: 5px;
      }
    }

    .actions {
      .md-button {
        margin: 0;
      }
    }
    .form {
      margin-bottom: 60px;
    }
    .background {
      background: url('/static/img/login-bg.svg');
      position: absolute;
      height: 100%;
      width: 100%;
      top: 0;
      bottom: 0;
      right: 0;
      left: 0;
      z-index: 0;
    }
    .md-content {
      z-index: 1;
      padding: 40px;
      width: 100%;
      max-width: 400px;
      position: relative;
    }
    .loading-overlay {
      z-index: 10;
      top: 0;
      left: 0;
      right: 0;
      position: absolute;
      width: 100%;
      height: 100%;
      background: rgba(255, 255, 255, 0.9);
      display: flex;
      align-items: center;
      justify-content: center;
    }
  }
</style>
