<template>
  <div>
    <md-card md-with-hover>
      <md-card-header>
        <div class="md-title">{{ name }}</div>
      </md-card-header>

      <md-card-content>
        <md-progress-bar class="goal-progress-bar" md-mode="determinate" :md-value="currentAmount / targetAmount * 100.0"></md-progress-bar>
        <div>
          <span>{{ currentAmount }}</span> / <span>{{ targetAmount }}</span>
        </div>
      </md-card-content>

    </md-card>
  </div>
</template>

<script>
  export default {
    name: 'SavingsGoalCard',
    props: ['name', 'currentAmount', 'targetAmount']
  }
</script>

<style lang="scss">
  .goal-progress-bar {
    height: 40px !important;
    margin-bottom: 10px;
  }
</style>
