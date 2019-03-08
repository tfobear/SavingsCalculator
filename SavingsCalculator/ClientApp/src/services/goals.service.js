import axios from 'axios'

const goalsService = {
  getSavingsGoals () {
    return new Promise((resolve, reject) => {
      axios.get('/api/savings')
        .then(res => {
          resolve(res.data)
        })
        .catch((reason) => {
          reject(reason)
        })
    })
  },
  addSavingsGoal (goal) {
    return new Promise((resolve, reject) => {
      axios.post('/api/savings', goal)
        .then(res => {
          resolve(res.data)
        })
        .catch(reason => {
          reject(reason)
        })
    })
  },
  updateSavingsGoal (goalId, goal) {
    return new Promise((resolve, reject) => {
      axios.put(`/api/savings?goalId=${goalId}`, goal)
        .then(res => {
          resolve(res.data)
        })
        .catch(reason => {
          reject(reason)
        })
    })
  },
  deleteGoal (goalId) {
    return new Promise((resolve, reject) => {
      axios.delete(`/api/savings?goalId=${goalId}`)
        .then(result => {
          resolve(result.data)
        })
        .catch(reason => {
          reject(reason)
        })
    })
  }
}

export default goalsService
