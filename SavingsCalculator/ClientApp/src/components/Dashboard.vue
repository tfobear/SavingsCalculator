<template>
  <div class="goals-container">
    <div v-if="!goals || goals.length === 0">
      <md-empty-state
        md-icon="attach_money"
        md-label="Create your first savings goal"
        md-description="Creating a savings goal, you'll be able to track how close to your target amount you are.">
        <md-button class="md-primary md-raised" @click="showAddGoalDialog = true">Create first goal</md-button>
      </md-empty-state>
    </div>
    <div v-for="goal in goals" v-else :key="goal.id">
      <savings-goal-card class="goal-card"
        :id="goal.id" :name="goal.name" :currentAmount="goal.currentAmount" :targetAmount="goal.targetAmount"
        @delete-goal="deleteGoal" />
    </div>
    <md-button class="add-button md-fab md-primary" @click="showAddGoalDialog = true" v-if="goals && goals.length > 0">
      <md-icon>add</md-icon>
    </md-button>

    <md-dialog :md-active.sync="showAddGoalDialog" class="add-goal-dialog">
      <md-dialog-title><md-icon>add</md-icon> Add Savings Goal</md-dialog-title>

      <form>
        <md-field>
          <label>Name</label>
          <md-input v-model='newGoal.name' autofocus></md-input>
          <span class="md-error" v-if="newGoalErrors.nameRequired">Name is required</span>
        </md-field>

        <md-field>
          <md-icon>attach_money</md-icon>
          <label>Target Amount</label>
          <md-input type="number" step="0.01" v-model='newGoal.targetAmount'></md-input>
          <span class="md-error" v-if="newGoalErrors.targetAmountRequired">Target Amount is required</span>
        </md-field>

        <md-field>
          <md-icon>attach_money</md-icon>
          <label>Current Amount</label>
          <md-input type="number" step="0.01" v-model='newGoal.currentAmount'></md-input>
          <span class="md-error" v-if="newGoalErrors.currentAmountRequired">Current Amount is required</span>
        </md-field>

        <span class="md-error" v-if="addSavingsGoalServerError">Something went wrong! Try again or call support.</span>
      </form>

      <md-dialog-actions>
        <md-button class="md-primary" @click="showAddGoalDialog = false">Close</md-button>
        <md-button class="md-primary" @click="saveGoal">Save</md-button>
      </md-dialog-actions>

      <md-snackbar md-position="center" md-duration="4000" :md-active.sync="showDeletedSnackbar" md-persistent>
        <span>Goal Deleted!</span>
      </md-snackbar>
      <md-snackbar md-position="center" md-duration="4000" :md-active.sync="showSomethingWrongSnackbar" md-persistent>
        <span>Something went wrong! Please try again or call support.</span>
      </md-snackbar>
    </md-dialog>
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
  checkForm: function (e) {
    e.preventDefault()
    this.newGoalErrors.nameRequired = false
    this.newGoalErrors.targetAmountRequired = false
    this.newGoalErrors.currentAmountRequired = false

    if (this.newGoal.name === '') {
      this.newGoalErrors.nameRequired = true
    }

    if (this.newGoal.targetAmount) {
      this.newGoalErrors.targetAmountRequired = true
    }

    if (this.newGoal.currentAmountRequired) {
      this.newGoalErrors.currentAmountRequired = true
    }
  },
  data () {
    return {
      goals: [],
      showAddGoalDialog: false,
      showDeletedSnackbar: false,
      showSomethingWrongSnackbar: false,
      newGoal: {
        name: '',
        currentAmount: null,
        targetAmount: null
      },
      newGoalErrors: {
        nameRequired: false,
        targetAmountRequired: false,
        currentAmountRequired: false
      },
      addSavingsGoalServerError: false
    }
  },
  methods: {
    loadSavingsGoals () {
      goalsService.getSavingsGoals().then((result) => {
        this.goals = result
      })
    },
    saveGoal () {
      goalsService.addSavingsGoal(this.newGoal)
        .then(result => {
          this.addSavingsGoalServerError = false

          this.newGoal.name = ''
          this.newGoal.currentAmount = null
          this.newGoal.targetAmount = null

          this.goals.push(result)
          this.showAddGoalDialog = false
        })
        .catch(() => {
          this.addSavingsGoalServerError = true
        })
    },
    deleteGoal (goalId) {
      goalsService.deleteGoal(goalId)
        .then(result => {
          this.goals = this.goals.filter(goal => goal.id !== goalId)
          this.showDeletedSnackbar = true
        })
        .catch(() => {
          this.showSomethingWrongSnackbar = true
        })
    }
  }
}
</script>

<style lang='scss'>
.add-button {
  position: fixed;
  top: calc(100% - 100px);
  right: 50px;
}

.goals-container {
  width: 720px;
  margin: 0 auto;
}

.goal-card {
  margin-bottom: 40px;
}

.add-goal-dialog {
  width: 380px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  form {
    justify-content: flex-start;
    max-width: 280px; // or however wide you want your content block to be
    width: 100%;

    .md-field {
      width: 280px !important;
    }
  }
}

</style>
