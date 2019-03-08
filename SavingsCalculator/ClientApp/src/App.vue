<template>
  <div>
    <header class="app-header" v-if="isLoggedIn">
      <md-toolbar class="md-primary">
        <div class="md-toolbar-section-start">
          <h1 class="md-title"> 💰 Savings Calculator </h1>
        </div>

        <div class="md-toolbar-section-end">
          <span>Logout</span>
          <md-button @click="logout" class="md-icon-button">
            <md-icon>exit_to_app</md-icon>
          </md-button>
        </div>
      </md-toolbar>
    </header>
    <main>
      <router-view></router-view>
    </main>
  </div>
</template>

<script>

export default {
  name: 'app',
  computed: {
    isLoggedIn: function () {
      return this.$store.getters.isLoggedIn
    }
  },
  methods: {
    logout: function () {
      this.$store.dispatch('logout').then(() => {
        this.$router.push('/login')
      })
    }
  }
}
</script>

<style>
body {
  margin: 0;
}

.content {
  min-height: 100%;
}

.app-header {
  margin-bottom: 60px;
}

.logout-button {
  cursor: pointer;
}
</style>
