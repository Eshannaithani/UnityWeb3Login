using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Web3WalletSendTransactionExample : MonoBehaviour
{
    public Text balanceText;

    public InputField sendersAddress;
    public InputField Amount;

    public Text WalletAddress;

    async public void OnSendTransaction()
    {
        fetchbalance();
        // https://chainlist.org/
        string chainId = "4"; // rinkeby
        // account to send to
        string to = "0xdD4c825203f97984e7867F11eeCc813A036089D1";
        // value in wei
        float a =float.Parse( Amount.text) * 1000000000000000000;
        print("float 1  " +a);
        print("float eth " + Convert.ToDecimal(a).ToString());
       // string value = "2300000000000000";
        string value = ""+ Convert.ToDecimal(a).ToString();
        print("value "+value);
        // data OPTIONAL
        string data = "";
        // gas limit OPTIONAL
        print(sendersAddress.text);
        string gasLimit = "21000";
        // gas price OPTIONAL
        string gasPrice = "";
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, sendersAddress.text, value, data, gasLimit, gasPrice);
        print(response);
        balanceText.text = response;
    }

    private void Start()
    {
        fetchbalance();
    }
    async public void fetchbalance()
    {
        string chain = "ethereum";
        string network = "rinkeby"; // mainnet ropsten kovan rinkeby goerli
        string account = "0xdD4c825203f97984e7867F11eeCc813A036089D1";

        WalletAddress.text = account;
        string balance = await EVM.BalanceOf(chain, network, PlayerPrefs.GetString("Account"));
        float a = float.Parse(balance) / 1000000000000000000;
        balanceText.text ="ETH "+ a;
        fetchbalancAll();

        fetchalltokens721();
    }
    async public void fetchbalancAll()
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x2ebecabbbe8a8c629b99ab23ed154d74cd5d4342";
        string[] accounts = { PlayerPrefs.GetString("Account"), "0xaCA9B6D9B1636D99156bB12825c75De1E5a58870" };
        string[] tokenIds = { "17", "22" };

        List<BigInteger> batchBalances = await ERC1155.BalanceOfBatch(chain, network, contract, accounts, tokenIds);
        foreach (var balance in batchBalances)
        {
            print("BalanceOfBatch: " + balance);
        }
    }
    async public void fetchalltokens721()
    {
        string chain = "ethereum";
        string network = "rinkeby"; // mainnet ropsten kovan rinkeby goerli
        string account = "0xebc0e6232fb9d494060acf580105108444f7c696";
        string contract = "";
        string response = await EVM.AllErc721(chain, network, PlayerPrefs.GetString("Account"), contract);
        print("res "+response);

    }
}
