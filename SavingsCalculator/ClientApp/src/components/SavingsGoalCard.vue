<template>
  <div class="container">
    <md-card md-with-hover>
      <form>
        <md-card-header>
          <div @mouseover="showNameEdit = true" @mouseout="showNameEdit = false" class="md-title">
            <span @click="editNameClick" v-show="!editName">{{ name }} &nbsp; <md-icon v-show="showNameEdit">edit</md-icon></span>
            <span v-show="editName">
              <md-field>
                <label>Name</label>
                <md-input ref="name" v-model="editGoal.name"></md-input>
                <span class="md-error" v-if="editGoalErrors.nameRequired">Name is required</span>
              </md-field>
            </span>
          </div>
        </md-card-header>

        <md-card-content>
          <md-progress-bar class="goal-progress-bar" md-mode="determinate" :md-value="currentAmount / targetAmount * 100.0"></md-progress-bar>
          <div class="amounts-container">
            <div @mouseover="showCurrentEdit = true" @mouseout="showCurrentEdit = false">
              <span @click="editCurrentAmountClick" v-show="!editCurrentAmount">
                <span class="strong">Current: </span><md-icon>attach_money</md-icon>
                <span>{{ currentAmount }}</span> &nbsp; <md-icon v-show="showCurrentEdit">edit</md-icon>
              </span>
              <span v-show="editCurrentAmount">
                <md-field>
                  <label>Current Amount</label>
                  <span class="md-prefix">$</span>
                  <md-input type="number" step="0.01" v-model='editGoal.currentAmount' ref="currentAmount"></md-input>
                  <span class="md-error" v-if="editGoalErrors.currentAmountRequired">Current Amount is required</span>
                </md-field>
              </span>
            </div>
            <div>/</div>
            <div @mouseover="showTargetEdit = true" @mouseout="showTargetEdit = false">
              <span @click="editTargetAmountClick" v-show="!editTargetAmount">
                <span class="strong">Target: </span><md-icon>attach_money</md-icon>
                <span>{{ targetAmount }}</span> &nbsp; <md-icon v-show="showTargetEdit">edit</md-icon>
              </span>
              <span v-show="editTargetAmount">
                <md-field>
                  <label>Target Amount</label>
                  <span class="md-prefix">$</span>
                  <md-input type="number" step="0.01" v-model='editGoal.targetAmount' ref="targetAmount"></md-input>
                  <span class="md-error" v-if="editGoalErrors.targetAmountRequired">Target Amount is required</span>
                </md-field>
              </span>
            </div>
          </div>
          <span class="md-error" v-if="saveGoalServerError">Something went wrong! Retry the action or call support.</span>
        </md-card-content>
      </form>
    </md-card>

    <md-button v-if="editName || editCurrentAmount || editTargetAmount" class="delete-button md-icon-button md-raised md-primary" @click="saveGoal">
      <md-icon>check</md-icon>
    </md-button>
    <md-button v-else class="delete-button md-icon-button md-raised md-accent" @click="deleteGoal">
      <md-icon>delete</md-icon>
    </md-button>
  </div>
</template>

<script>
  export default {
    name: 'SavingsGoalCard',
    props: ['id', 'name', 'currentAmount', 'targetAmount', 'saveGoalResult'],
    created () {
      this.editGoal.name = this.name
      this.editGoal.currentAmount = this.currentAmount
      this.editGoal.targetAmount = this.targetAmount
    },
    methods: {
      saveGoal () {
        this.$emit('save-goal', this.id, {
          name: this.editGoal.name,
          currentAmount: this.editGoal.currentAmount,
          targetAmount: this.editGoal.targetAmount
        })
      },
      deleteGoal () {
        this.$emit('delete-goal', this.id)
      },
      editNameClick () {
        this.editName = true
        this.$nextTick(function () {
          this.$refs.name.$el.select()
          this.$refs.name.$el.focus()
        })
      },
      editCurrentAmountClick () {
        this.editCurrentAmount = true
        this.$nextTick(function () {
          this.$refs.currentAmount.$el.select()
          this.$refs.currentAmount.$el.focus()
        })
      },
      editTargetAmountClick () {
        this.editTargetAmount = true
        this.$nextTick(function () {
          this.$refs.targetAmount.$el.select()
          this.$refs.targetAmount.$el.focus()
        })
      }
    },
    watch: {
      saveGoalResult: function (newVal, oldVal) {
        if (newVal === 'success') {
          this.editName = false
          this.editCurrentAmount = false
          this.editTargetAmount = false
          this.saveGoalServerError = false
          this.saveGoalResult = 'none'
        } else if (newVal === 'failure') {
          this.saveGoalServerError = true
        }
      }
    },
    data () {
      return {
        refs: {
          name: null,
          currentAmount: null,
          targetAmount: null
        },
        showNameEdit: false,
        showCurrentEdit: false,
        showTargetEdit: false,
        editName: false,
        editCurrentAmount: false,
        editTargetAmount: false,
        editGoal: {
          name: '',
          currentAmount: 0.0,
          targetAmount: 0.0
        },
        editGoalErrors: {
          nameRequired: false,
          targetAmountRequired: false,
          currentAmountRequired: false
        },
        saveGoalServerError: false
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

  .amounts-container {
    font-size: 16px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex-direction: row;
  }

  .strong {
    font-weight: bold;
  }
</style>
