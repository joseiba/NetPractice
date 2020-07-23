import React, { Component } from "React";
import { Text, StyleSheet } from "react-native";
import { Content, Button, Icon } from "native-base";

export default class DrawMenu extends Component{
    render(){
        return(
            <Content style={styles.menuWrapper}>
            
                <Button transparent iconLetf full onPress={() => this.props.navigation.navigate('Home')}
                    style={styles.menuList}>
                        <Icon type='FontAwesone' name='home' style={styles.menuIcon} />
                        <Text styles={styles.menuItem}>Dashboard</Text>
                </Button>

                <Button transparent iconLetf full={() => this.props.navigation.navigate('Contact')} 
                    style={styles.menuList}>
                        <Icon type='FontAwesome' name='user' style={styles.menuIcon}/>
                        <Text style={styles.menuItem}>Contact</Text>
                </Button>
            
            </Content>
        )
    }

}

 const styles = StyleSheet.create({
        menuWrapper: {
            marginTop: 1rem
        },

        menuList: {
            justifyContent: 'flex-start'
        },

        menuItem: {
            fontSize: 15,
            color: '#ffff',
            flex: 0.95
        },

        menuIcon: {
            color: '#ffff',
            flex: 0.15
        }
    });