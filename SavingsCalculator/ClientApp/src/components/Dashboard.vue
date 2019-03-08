<template>
  <div class="container">
    <div v-if="!goals || goals.length === 0">
      <md-empty-state
        md-icon="attach_money"
        md-label="Create your first savings goal"
        md-description="Creating a savings goal, you'll be able to track how close to your target amount you are.">
        <md-button class="md-primary md-raised">Create first goal</md-button>
      </md-empty-state>
    </div>
    <div v-for="goal in goals" v-else :key="goal.id">
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
