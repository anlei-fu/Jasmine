<template>
  <div>
    <Divider orientation="left">Input</Divider>
    <!-- input-->
    <Row>
      <!--path -->
      <Col span="8">
        <Input v-model="path" placeholder="Service path">
          <Select slot="prepend" style="width: 80px">
            <Option value="http">http://</Option>
            <Option value="https">https://</Option>
          </Select>
        </Input>
      </Col>
      <!-- method -->
      <Col span="3">
        <Select style="width: 80px" v-model="method" placeholder="Method">
          <Option value="get">Get</Option>
          <Option value="post">Post</Option>
          <Option value="put">Put</Option>
          <Option value="delete">Delete</Option>
          <Option value="trace">Trace</Option>
          <Option value="option">Option</Option>
        </Select>
      </Col>
      <!-- encoding -->
      <Col span="2">
        <Select style="width: 100px" v-model="encoding" placeholder="Encoding">
          <Option value="utf-8">utf-8</Option>
          <Option value="gb2312">gb2312</Option>
          <Option value="gbk">gbk</Option>
        </Select>
      </Col>
    </Row>
    <!-- Body And Param -->
    <Row>
      <!-- Body -->
      <Col span="6">
        <Row>
          <Col span="24">
            <Divider orientation="left">Body</Divider>
          </Col>
        </Row>
        <Row>
          <Col span="24">
            <Input
              v-model="body"
              type="textarea"
              placeholder="Enter Json string or else...."
              autosize="true"
            />
          </Col>
        </Row>
      </Col>
      <!-- param -->
      <Col span="6" style="margin-left:80px;">
        <Row>
          <Col span="24">
            <Divider orientation="left">Params</Divider>
          </Col>
        </Row>
        <Row>
          <Col span="24">
            <Input
              v-model="params"
              type="textarea"
              placeholder="Enter params string..."
              autosize="true"
            />
          </Col>
        </Row>
      </Col>
    </Row>
    <!-- Body And Param -->
    <Row>
      <!-- Body -->
      <Col span="6">
        <Row>
          <Col span="24">
            <Divider orientation="left">Headers</Divider>
          </Col>
        </Row>
        <Row>
          <Col span="24">
            <Input
              v-model="headers"
              type="textarea"
              placeholder="Enter Header string  splite by ';'"
              autosize="true"
            />
          </Col>
        </Row>
      </Col>
      <!-- param -->
      <Col span="6" style="margin-left:80px;">
        <Row>
          <Col span="24">
            <Divider orientation="left">Cookies</Divider>
          </Col>
        </Row>
        <Row>
          <Col span="24">
            <Input
              v-model="cookies"
              type="textarea"
              placeholder="Enter cookie item splite by ';'"
              autosize="true"
            />
          </Col>
        </Row>
      </Col>
    </Row>

    <Row style="margin-top:50px;text-align:left;">
      <Button type="primary" style="width:350px;" @click="test">测试</Button>
    </Row>
    <!-- output -->
    <Divider orientation="left">Output</Divider>
    <Row>
      <Col></Col>
      <Col></Col>
    </Row>
    <Row>
      <Col span="6" style="text-align:left">
        <Divider orientation="left">Code</Divider>
        Code:{{result.code}},Elapse:{{result.elapse}}
      </Col>
      <Col span="6" style="text-align:left">
        <Divider orientation="left">Data</Divider>
        {{result.code === 200 ? result.data:result.error}}
      </Col>
    </Row>
  </div>
</template>
<script>
export default {
  data() {
    return {
      path: "",
      method: "",
      params: "",
      body: "",
      cookies: "",
      headers: "",
      encoding: "",
      startTime: {},
      stopTime: {},
      timeout: 10000,
      result: {
        code: 200,
        data: "dddd",
        error: "ssss",
        elapse: 222
      }
    };
  },
  methods: {
      getParams() {
      var map = new Map();
      this.params.split("&").forEach(element => {
        alert(element);
        var pair = element.split("=");
        map.set(pair[0], pair[1]);
      });
      return map;
    },
    test() {
      switch (this.method) {
        case "get":
          let parameter = this.getParams();
          alert(this.path);
          this.$store.state.client
            .get(this.path, {
              params: parameter,
              timeout: this.timeout
            })
            .then(response => {
              console.log(response);
              console.log("after response received!");
              this.result.code = response.status;
              this.result.data = response.data;
            })
            .catch(_err => {
              alert("error received!");
              alert(_err);
              console.log(_err);
              console.log("error logged!");
              this.result.error = _err;
            });
          break;
        case "post":
          break;
        case "delete":
          break;
        case "put":
          break;
        case "trace":
          break;
        case "option":
          break;
      }

      alert("after call");
    },
    getHeaders() {},
    getCookies() {}
  }
};
</script>
