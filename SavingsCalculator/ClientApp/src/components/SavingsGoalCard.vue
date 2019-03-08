<template>
  <div class="container">
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
    <md-button class="delete-button md-icon-button md-raised md-accent" @click="deleteGoal">
      <md-icon>delete</md-icon>
    </md-button>
  </div>
</template>

<script>
  export default {
    name: 'SavingsGoalCard',
    props: ['id', 'name', 'currentAmount', 'targetAmount'],
    methods: {
      deleteGoal () {
        this.$emit('delete-goal', this.id)
      }
    }
  }
</script>

<style lang="scss">
  .goal-progress-bar {
    height: 40px !important;
    margin-bottom: 10px;
  }

  .container {
    position: relative;
  }

  .container .delete-button {
    position: absolute;
    top: -20px;
    right: -20px;
  }
</style>
