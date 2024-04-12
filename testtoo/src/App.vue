<template>
  <div class="page-wrapper">
    <div class="custom-background">
      <h2 class="custom-title">Pöördumised</h2>
      <form @submit.prevent="addIssue" class="form">
        <div class="form-group">
          <label for="description" class="label">Kirjeldus:</label>
          <input type="text" id="description" v-model="newIssue.description" required placeholder="Pöördumise kirjeldus" class="input-field">
        </div>
        <div class="form-group">
          <label for="deadline" class="label">Lahendamise tähtaeg:</label>
          <input type="datetime-local" id="deadline" v-model="newIssue.deadline" required placeholder="pp.kk.aaaa, tt:mm" class="input-field">
        </div>
        <button type="submit" class="submit-button">Lisa Pöördumine</button>
      </form>

      <div class="issues-container">
        <ul class="issue-list">
          <li v-for="issue in sortedIssues" :key="issue.id" :class="{ 'expired': isExpired(issue.deadline) }" class="issue-item">
            <div class="issue-info">
              <strong class="label">Kirjeldus:</strong>
              <span>{{ issue.description }}</span>
            </div>
            <div class="issue-info">
              <strong class="label">Sisestamise aeg:</strong>
              <span>{{ formatDateTime(issue.insertionTime) }}</span>
            </div>
            <div class="issue-info">
              <strong class="label">Lahendamise tähtaeg:</strong>
              <span>{{ formatDateTime(issue.deadline) }}</span>
            </div>
            <button @click="markResolved(issue.id)" class="resolve-button">Märgi lahendatuks</button>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      issues:[],
      newIssue: {
        description: '',
        deadline: ''
      }
    };
  },
  computed: {
    sortedIssues() {
      return this.issues.sort((a, b) => new Date(a.deadline) - new Date(b.deadline));
    }
  },
  methods: {
    isExpired(deadline) {
      const currentTime = new Date();
      const deadlineTime = new Date(deadline);
      const timeDifference = deadlineTime.getTime() - currentTime.getTime();
      return timeDifference <= 0 || timeDifference < 1000 * 60 * 60;
    },
    getIssues() {
      axios.get('http://localhost:7055/api/Inquiry')
          .then(response => {
            this.issues = response.data;
          })
          .catch(error => {
            console.error('Error fetching issues:', error);
          });
    },
    formatDateTime(dateTime) {
      const date = new Date(dateTime);
      const hours = date.getHours();
      const minutes = date.getMinutes().toString().padStart(2, '0'); 

      return `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}, ${hours}:${minutes}`;
    },

    formatDate(date) {
      const formattedDate = new Date(date);
      const day = String(formattedDate.getDate()).padStart(2, '0');
      const month = String(formattedDate.getMonth() + 1).padStart(2, '0');
      const year = formattedDate.getFullYear();

      return `${day}.${month}.${year}`;
    },
    addIssue() {
      axios.post('http://localhost:7055/api/Inquiry', this.newIssue)
          .then(response => {
            this.issues.push(response.data);
            this.newIssue.description = '';
            this.newIssue.deadline = '';
          })
          .catch(error => {
            console.error('Error adding issue:', error);
          });
    },
    markResolved(id) {
      axios.delete(`http://localhost:7055/api/Inquiry/${id}`)
          .then(() => {
            this.issues = this.issues.filter(issue => issue.id !== id);
          })
          .catch(error => {
            console.error('Error deleting issue:', error);
          });
    }
  },
  mounted() {
    
    this.getIssues();
  }
};
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Bebas+Neue&display=swap');

body {
  background-image: url('/taust.png');
  background-size: cover;
  background-attachment: fixed;
  padding: 20px;
}
.custom-background {
  background-color: #65a5f8;
  padding: 20px;
  border-radius: 50px;
  align-items: center;
  justify-content: center;
  margin: 20px auto;

}
.page-wrapper {
  display: flex;
  justify-content: flex-start;
  align-items: flex-start;
  min-height: 100vh;
}
.custom-title {
  color: #ffffff;
  font-family: 'Bebas Neue', sans-serif;
  font-size: 50px;
  margin-left: 200px;
}
.form-group {
  margin-bottom: 15px;
  display: flex;
}
.label {
  font-size: 22px;
  font-family: 'Bebas Neue', sans-serif;
  width: 200px;
  display: inline-block;
}
.input-field {
  border: 1px solid #ccc;
  border-radius: 5px;
  padding: 8px;
  width: 300px;
  margin-left: -30px;
  flex: 1;
}
.submit-button {
  background-color: #011d5e;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 10px;
  margin: auto;
  margin-left: 225px;
}
.issues-container {
  width: 600px;
  overflow-x: auto;
}
.issue-list {
  list-style-type: none;
  padding: 0;
}
.issue-item {
  margin-bottom: 15px;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
}
.issue-info {
  margin-bottom: 5px;
  font-family: 'Bebas Neue', sans-serif;
}
.resolve-button {
  background-color: #001372;
  color: white;
  padding: 5px 10px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-left: 220px;
}
.expired {
  color: rgba(255, 0, 0, 0.99);
}
@media only screen and (max-width: 767px) {
  @import url('https://fonts.googleapis.com/css2?family=Bebas+Neue&display=swap');
  body {
    background-image: url('/phone.png');
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
    max-width: 400px;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: -1;
    overflow-x: hidden;
  }

  .custom-background {
    background-color: #65a5f8;
    padding: 20px;
    align-items: center;
    border-radius: 50px;
    margin: 20px auto;
    width: calc(100% - 40px);
    max-width: 400px; 
    overflow-x: hidden;
  }

  .page-wrapper {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 100vh;
    overflow-x: hidden;
  }

  .custom-title {
    color: #ffffff;
    font-family: 'Bebas Neue', sans-serif;
    font-size: 50px;
    text-align: center;
    margin-left: 20px;
  }

  .form-group {
    margin-bottom: 15px;
  }

  .label {
    font-size: 20px;
    font-family: 'Bebas Neue', sans-serif;
    width: 100%;
    display: inline-block;
  }

  .input-field {
    border: 1px solid #ccc;
    border-radius: 5px;
    padding: 8px;
    width: 100%;
    box-sizing: border-box;
  }

  .submit-button {
    background-color: #011d5e;
    color: white;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    display: block;
    margin: 20px auto;
  }

  .issues-container {
    width: 100%;
    overflow-x: auto;
    padding: 0 20px;
    margin-left: -20px;
  }

  .issue-list {
    list-style-type: none;
    padding: 0;
  }

  .issue-item {
    margin-bottom: 15px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }

  .issue-info {
    margin-bottom: 5px;
  }

  .resolve-button {
    background-color: #001372;
    color: white;
    padding: 5px 10px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    display: block;
    margin: 10px auto;
  }

  .expired {
    color: rgba(255, 0, 0, 0.99);
  }
}
</style>


