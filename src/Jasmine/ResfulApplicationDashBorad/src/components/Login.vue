npm<style>
.outter-wrapper {
  vertical-align: middle;
  width: 100%;
  height: 100%;
  margin: auto;
  background-color: rgb(95, 95, 95);

  justify-content: center;
}
.login-container {
  margin: auto;
  width: 450px;
  height: 300px;
  background-color: rgb(235, 235, 235);
  position: relative;

  top: 25%;
}
.input-box2 {
  margin-top: 15px;
  font-size: 1.15em;
}
.input-box1 {
  margin-top: 30px;
  font-size: 1.15em;
}
.check-box {
  margin-top: 10px;
}

.login-title {
  text-align: left;
  margin-left: 10px;
  margin-bottom: 10px;
  padding: 10px;
}
.divider {
  margin-top: 10px;
}
.button {
  margin-top: 10px;
}
</style>

<template>
  <div class="outter-wrapper">
    <div class="login-container">
      <div class="login-title">
        <Row>
          <h2 style>登录</h2>
        </Row>
      </div>
      <Divider style="margin:0;background-color:#2db7f5;height:2px;margin-bottom:10px;"/>
      <div class="input-box1">
        <Row>
          <Input
            prefix="ios-contact"
            placeholder="Enter user name"
            style="width:250px;font-size:15px"
            v-model="this.$store.state.user.name"
          />
        </Row>
      </div>
      <div class="input-box2">
        <Row>
          <Input
            prefix="ios-keypad"
            placeholder="Enter password"
            type="password"
            style="width: 250px;font-size-15px;"
            v-model="this.$store.state.user.password"
          />
        </Row>
        <div class="check-box" style="text-align:left;">
          <Row>
            <Checkbox label="twitter" style="width:250px; margin-left:25%;">记住密码</Checkbox>
          </Row>
        </div>
        <div class="button">
          <Row>
            <Button style="width:250px;" type="info" :click="login">登录</Button>
          </Row>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {};
  },
  methods: {
    login() {
      // login
      if (
        this.$store.state.visiter.getData(
          "/login?user=" +
            this.$store.state.user.name +
            "&password=" +
            this.$store.state.user.password
        )
      ) {
        this.$store.state.isSignIned = true;
        var groups = this.$store.state.visitor.getData("/getall");
        if (groups) {
          this.$store.commit("refreshAll", this.mapGroups(groups));
          this.$router.push({ path: "/main/summary" });
        } else {
          this.$store.state.msgBox.show();
        }
      } else {
        this.$store.state.msgBox.show();
      }
    }
  }
};
</script>
