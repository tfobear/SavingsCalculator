<template>
  <div class="container">
    <div v-for="goal in goals" :key="goal.id">
      <savings-goal-card class="goal-card" :name="goal.name" :currentAmount="goal.currentAmount" :targetAmount="goal.targetAmount" />
    </div>
  </div>
</template>

<script>
import SavingsGoalCard from './SavingsGoalCard.vue'
import goalsService from '../services/goals.service.js'

export default {
  name: 'Dashboard',
  created () {
    this.loadSavingsGoals()
  },
  components: {
    'savings-goal-card': SavingsGoalCard
  },
  data () {
    return {
      goals: []
    }
  },
  methods: {
    loadSavingsGoals () {
      goalsService.getSavingsGoals().then((result) => {
        this.goals = result.data
      })
    }
  }
}
</script>


<style lang='scss'>
.container {
  width: 720px;
  margin: 0 auto;
}

.goal-card {
  margin-bottom: 20px
}
</style>
