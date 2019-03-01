<template>
  <div>
    <header v-if="isLoggedIn">
      <md-toolbar class="md-primary">
        <div class="md-toolbar-section-start">
          <h1 class="md-title">Vue Material</h1>
        </div>

        <div class="md-toolbar-section-end">
          <a @click="logout">Logout</a>
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
      console.log(this.$store)
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
</style>
